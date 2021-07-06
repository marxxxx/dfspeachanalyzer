# Introduction

This project shows training and application of a custom machine learning model trained with the ML.NET Model Builder to predict political party affinity based on Speach/Interview Input data.

# Projects

## DataProvider
A Console App using Selenium to crawl the WebSite of the Austrian National Parliament for Speaches of the National Assembly, classifies them and stores Speach Content along with Party of the Speaker in a Local SQL Server Database.

## WebApp
ASP.NET Core Razor Pages Web App presenting the Frontend and uses the trained model to display Prediction Results based on User Input.

## Model 
A Console App containing the trained model used for testing.

# Training Dataset
The training dataset was aquired from the WebSite of the Austrian National Parliament at https://www.parlament.gv.at/PAKT/STPROT/
The training dataset is part of the repository (SpeachDb.7z).

# Disclaimer
This project shows a sample usage of the ML.NET Model Builder and does not aim to provide a perfect ML Model for the given Use Case. 
The Model was trained for 10 Minutes on my Laptop.
