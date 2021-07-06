# Introduction

This project shows training and application of a custom machine learning model trained with the ML.NET Model Builder to predict political party affinity based on Speach/Interview Input data.

# Projects

## DataProvider
A Console App using Selenium to crawl the WebSite of the Austrian National Parlament for Speaches of the National Assembly, classifies them and stores Speach Content along with Party of the Speaker in a Local SQL Server Database.

## WebApp
ASP.NET Core Razor Pages Web App presenting the Frontend and uses the trained model to display Prediction Results based on User Input.

## Model 
A Console App containing the trained model used for testing.
