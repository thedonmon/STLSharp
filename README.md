# STLSharp
STLParser in C# via API

<H1>STL Parser Project by David Maman</H1>
<p>This project runs via loading an STL file from your local machine and will parse it if its in Binary or ASCII. Binary has some issues and will not work.
There is no error handling in v1 as file is expected to be in correct format. You can POST this file via API call to the <code>https://localhost:5001/stlparser</code> endpoint.
use the JSON object: <br/> <code>{
   "path": "/user/Documents/ASCII.stl"
}</code>
</br>
And make the POST call to see the results. 
</p>
Steps to run on any environment:
<ul>
<li>1. Download/Open Visual Studio Code or Visual Studio</li>
<li>2. Clone this repository to run on your local machine.</li>
<li>3. In VSCode it may ask you to insall the C# plugin, this is not necessary but can help.</li>
<li>4. Install dotnetcore cli if you don't have it already https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install </li>
<li>5. Once the project is open in VSCode, navigate to the terminal window inside VSCode and run <code>dotnet build</code> </li>
<li>6. If the build is successful, then run <code>dotnet run</code>. If this complains then use </br> <code>dotnet run -p "{yourLocalpathToProjectFile}/STLParser.csproj"</code></li>
<li>7. Grab an ASCII formatted STL file and get the full file path copied to clipboard or saved somewhere for quick access </li>
<li>8. Open up Postman and make the post call with the instructions above.</li>
</ul>

<p>If there are any issues feel free to reach out!</p>
