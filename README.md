# Forklift Control System

A full-stack application for managing robotic forklift fleets with command parsing capabilities.

## Architecture

- **Backend**: ASP.NET Core 8.0 Web API with Entity Framework Core
- **Frontend**: React 18 with TypeScript
- **Database**: SQLite with Entity Framework migrations
- **Data Source**: JSON file import with automatic reload on restart

## Features

### Exercise 1: Fleet Management
- Import forklift data from JSON file
- Store data in SQLite database
- Display fleet information in responsive table
- Automatic data refresh on backend restart

### Exercise 2: Command Parser
- Parse movement commands (F/B/L/R with values)
- Validate turn angles (90° multiples, 0-360° range)
- Display human-readable command descriptions
- Real-time input validation and error handling

## Quick Start

### Backend
```bash
cd ForkliftControl
dotnet run
```
API available at: `http://localhost:5232`
Swagger UI: `http://localhost:5232/swagger`

### Frontend
```bash
cd frontend
npm install
npm start
```
React app available at: `http://localhost:3000`

## API Endpoints

- `GET /api/forklifts` - Retrieve all forklifts

## Command Format

Commands consist of letter-number pairs:
- `F10` - Move Forward 10 metres
- `B5` - Move Backward 5 metres  
- `L90` - Turn Left 90 degrees (counterclockwise)
- `R180` - Turn Right 180 degrees (clockwise)

Example: `F10R90L90B5` produces:
1. Move Forward by 10 metres
2. Turn Right by 90 degrees
3. Turn Left by 90 degrees
4. Move Backward by 5 metres

## Data Management

The system automatically:
- Clears existing database data on startup
- Reloads from `forklifts.json` file
- Ensures data consistency between restarts

## Error Handling

- Comprehensive validation for command inputs
- Graceful handling of database initialization failures
- JSON parsing error recovery
- Network request error management

## Code Quality Features

- TypeScript for type safety
- Comprehensive error handling
- Performance optimizations (regex caching)
- Clean separation of concerns
- Detailed code documentation