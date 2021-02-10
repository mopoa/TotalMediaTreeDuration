<h1>Total Directory Tree Media Duration(win32)</h1>
<p><font face=tahoma size="2">
Are You wanna know your Video Course Total Length ??!!<br/>
this Program gives you the total media time of a directory tree

just Publish it with .NETCore and you are Done



</font>
</p>
<p>piece of code</p>
```CSharp

   TimeSpan DirectoryTreeTotalMediaDuration(string path)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0);
                IEnumerator localDirectories = Directory.EnumerateDirectories(path).GetEnumerator();
                while (localDirectories.MoveNext())
                {
                    duration = duration + DirectoryTreeTotalMediaDuration(localDirectories.Current.ToString());
                    IEnumerator files = Directory.EnumerateFiles(localDirectories.Current.ToString()).GetEnumerator();
                    System.Console.WriteLine(localDirectories.Current.ToString());
                    while (files.MoveNext())
                    {
                        int fileLength = (int)wmp.newMedia(files.Current.ToString()).duration;
                        TimeSpan fileLengthspan = new TimeSpan(0, 0, fileLength);
                        duration = duration + fileLengthspan;
                    }
                }
            return duration;
        }

```
