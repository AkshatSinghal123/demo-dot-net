# KCC Manufacturing Demo Project

## Project Structure
```
KCCDemoProject/
├── KCCDemoProject.csproj
├── Program.cs
├── Pages/
│   ├── Index.cshtml
├── wwwroot/
│   ├── css/
│   │   ├── site.css
└── README.md
```

## How to Run
1. Install .NET 6 SDK: [Download here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. Clone or extract the project.
3. Navigate to the project folder:
   ```sh
   cd KCCDemoProject
   ```
4. Restore dependencies:
   ```sh
   dotnet restore
   ```
5. Build the project:
   ```sh
   dotnet build
   ```
6. Run the application:
   ```sh
   dotnet run
   ```
7. Open your browser and visit:
   - [http://localhost:5000](http://localhost:5000)

## Publish the Application
To create a deployable package, run:
```sh
dotnet publish -c Release -o ./publish
```