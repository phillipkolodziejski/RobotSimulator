# Robot Simulator
## Authors

* **Phillip Kolodziejski**

## Description
The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units.

Built using dotnet core v2.1x

# Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

## Using the dotnet CLI

### Build
```
dotnet build
```

### Test
Run unit tests from `RobotSimulator.Tests` directory
```
dotnet test
```

## Docker

### Build 
```
docker-compose build
```

## Usage

The application supports 2 modes. Automated and Interactive. 

Application can read in commands of the following form: 

```
PLACE X,Y,F 
MOVE 
LEFT 
RIGHT 
REPORT
```

### 1. Automated mode
Commands can be provided in the file ```instructions.txt``` following the format above.

```
dotnet run automated instructions.txt
```
or 
```
docker-compose up
```

by default the application will read commands from `instructions.txt` but any filename can be specified instead. Instructions will be executed sequentially.

### 2. Interactive mode

Application will listen and accept input from the user. Accepted commands are specifed above.

```
dotnet run interactive
```

## Example instructions

```
PLACE 0,0,NORTH
MOVE 
REPORT
```

```
PLACE 1,2,EAST 
MOVE 
MOVE 
LEFT 
MOVE 
REPORT
```

Move in a square and return to starting postion and facing
```
PLACE 0,0,NORTH
MOVE
MOVE
RIGHT
MOVE
MOVE
RIGHT
MOVE
MOVE
RIGHT
MOVE
MOVE
RIGHT
REPORT
```

