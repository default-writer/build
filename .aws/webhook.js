'use strict';

/**
 * Follow these steps to configure the webhook in Slack:
 *
 *   1. Navigate to https://<your-team-domain>.slack.com/services/new
 *
 *   2. Search for and select "Incoming WebHooks".
 *
 *   3. Choose the default channel where messages will be sent and click "Add Incoming WebHooks Integration".
 *
 *   4. Copy the webhook URL from the setup instructions and use it in the next section.
 *
 *
 * To encrypt your secrets use the following steps:
 *
 *  1. Create or use an existing KMS Key - http://docs.aws.amazon.com/kms/latest/developerguide/create-keys.html
 *
 *  2. Click the "Enable Encryption Helpers" checkbox
 *
 *  3. Paste <SLACK_HOOK_URL> into the kmsEncryptedHookUrl environment variable and click encrypt
 *
 *  Note: You must exclude the protocol from the URL (e.g. "hooks.slack.com/services/abc123").
 *
 *  4. Give your function's role permission for the kms:Decrypt action.
 *      Example:

{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "Stmt1443036478000",
            "Effect": "Allow",
            "Action": [
                "kms:Decrypt"
            ],
            "Resource": [
                "<your KMS key ARN>"
            ]
        }
    ]
}

 */

const url = require('url');
const https = require('https');

// The base-64 encoded, encrypted key (CiphertextBlob) stored in the kmsEncryptedHookUrl environment variable
//const kmsEncryptedHookUrl = process.env.kmsEncryptedHookUrl;
const hookUrl = process.env.hookUrl;
// The Slack channel to send a message to stored in the slackChannel environment variable
const slackChannel = process.env.slackChannel;
//let hookUrl;


function postMessage(message, callback) {
    const body = JSON.stringify(message);
    const options = url.parse(hookUrl);
    options.method = 'POST';
    options.headers = {
        'Content-Type': 'application/json',
        'Content-Length': Buffer.byteLength(body),
    };

    const postReq = https.request(options, (res) => {
        const chunks = [];
        res.setEncoding('utf8');
        res.on('data', (chunk) => chunks.push(chunk));
        res.on('end', () => {
            if (callback) {
                callback({
                    body: chunks.join(''),
                    statusCode: res.statusCode,
                    statusMessage: res.statusMessage,
                });
            }
        });
        return res;
    });

    postReq.write(body);
    postReq.end();
}

function processEvent(event, callback) {
    const message = event;
    const slackMessage = {
        channel: slackChannel,
        user: "hack2root",
        text: message.detail["additional-information"].environment['image'],
        "attachments": [
        {
            "title": message["source"],
            "title_link": message.detail["additional-information"].logs['deep-link'],
            "pretext": "<" + message.detail["additional-information"].source['location'] + "|" + message.detail["project-name"] + ">" + " at *" + message.detail["additional-information"]["build-start-time"] + "*" + " " + message.detail['build-status'].toLowerCase()  + " " + " for " + message.detail["additional-information"].source['type'] + ' ' + "<https://github.com/hack2root/build/commit/" + message.detail["additional-information"]["source-version"] + "|" + message.detail["additional-information"]["source-version"] + ">",
            "text":  message['detail-type'],
            "mrkdwn_in": [
                "text",
                "pretext"
            ],
            "footer": message.detail["additional-information"].initiator + " " + message.detail["additional-information"]["source"]["location"] + " " + message["region"],
            "footer_icon": "https://platform.slack-edge.com/img/default_application_icon.png",
            "ts": Date.now()/1000
        }]
    };

    postMessage(slackMessage, (response) => {
        if (response.statusCode < 400) {
            console.info('Message posted successfully');
            callback(null);
        } else if (response.statusCode < 500) {
            console.error(`Error posting message to Slack API: ${response.statusCode} - ${response.statusMessage}`);
            callback(null);
        } else {
            callback(`Server error when processing message: ${response.statusCode} - ${response.statusMessage}`);
        }
    });
}


exports.handler = (event, context, callback) => {
    if (hookUrl) {
        processEvent(event, callback);
    } else {
        callback('Hook URL has not been set.');
    }
};
