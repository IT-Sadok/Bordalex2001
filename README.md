# Bordalex2001

A full-stack Booking System API with .NET backend and React frontend.

## Overview

Backend API for managing bookings and scheduling, deployed on Azure with a modern React client application.

## 🏗️ Tech Stack

- **Backend**: .NET 9, C#
- **Frontend**: React, TypeScript, Vite
- **Cloud**: Azure (Poland Central)
- **CI/CD**: GitHub Actions

## 🚀 Quick Start

### Backend

```bash
git clone https://github.com/IT-Sadok/Bordalex2001.git
cd Bordalex2001
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release
```

### Frontend

```bash
cd BookingSystemClient
npm install
npm run dev
```

## 📋 Features

- RESTful API for booking management
- React TypeScript frontend
- Automated testing and CI/CD pipeline
- Security vulnerability scanning
- Azure cloud deployment

## 🔄 CI/CD

GitHub Actions automatically:
- Builds and tests on push/PR to `dev` or `main`
- Runs security checks
- Publishes artifacts to Azure

## 🌐 Live API

https://bookingsystemapi-h9ftgjg0d7dcg3cq.polandcentral-01.azurewebsites.net

## 📝 Contributing

1. Create feature branch from `dev`
2. Make changes and ensure tests pass
3. Submit pull request

## 👥 Organization

[IT-Sadok](https://github.com/IT-Sadok)
