#!/usr/bin/env sh


for i in {0..200}
do
  filename="output_${i}.txt"

  echo 'newtonsoft 13.0.1' >> "$filename"
  dotnet test --configuration=release librarytest >> "$filename"
  #sleep "${expr 60 \* 5}"
  sleep 120

  echo 'newtonsoft 10.0.3' >> "$filename"
  dotnet test --configuration=release librarytest2 >> "$filename"
  #sleep "${expr 60 \* 5}"
  sleep 120
done