# Read the entire file to an array of bytes.

bytes = $(env:BuildKey)
# Decode first 12 bytes to a text assuming ASCII encoding.
$text = [System.Text.Encoding]::ASCII.GetString($bytes, 0, 12)