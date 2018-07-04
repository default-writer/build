if([System.IO.Directory]::Exists(".nuget\nuget")) {
  # file with path $path doesn't exist
  rm -recurse -force nuget
}
& .\.nuget\_NuGet
