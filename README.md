# SeunOrderNotification

## Prerequisites
```bash
- .NET Core SDK (version 8.0 or later) installed on your machine  
- A code editor or IDE of your choice (e.g., Visual Studio, Visual Studio Code)  
- Git installed on your machine
```

## Cloning the Repository

### Step 1: Clone the Repository
```bash
git clone https://github.com/Vastro-lorde/SeunOrderNotification
```

---

## Restoring Dependencies and Building the Project

### Step 1: Navigate to the Cloned Repository Directory
```bash
cd SeunOrderNotification
```

### Step 2: Restore NuGet Packages and Dependencies
```bash
dotnet restore
```

### Step 3: Build the Project
```bash
dotnet build
```

---

## Running the Application

### Step 1: Run the Application
```bash
dotnet run
```

### Step 2: Open the Application in a Web Browser
Open a web browser and navigate to [https://localhost:7233](http://localhost:5282) (or the URL specified in the `launchSettings.json` file).

---

## Troubleshooting

- If you encounter issues with NuGet package restoration, try running:  
  ```bash
  dotnet restore --force
  ```
  This forces a re-installation of packages.

- If you encounter issues with building the project, try running:  
  ```bash
  dotnet build --verbose
  ```
  This provides more detailed output for debugging.

---

## Additional Notes

- Make sure to update the `appsettings.json` file with your own Jwt configuration settings as needed.
