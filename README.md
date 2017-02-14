
# DressForSuccess

Commandline tool to properly dress for hot or cold weather before leaving the house.


## Install

Typically don't include .sln files, but have pushed the Visual Studio solution file for ease of launch so developer can see and run unit tests that were used during TDD.

## Run the tests

Basic unit tests exist for supporting modules:  DressOptionsTest and DressRulesTest.
TDD was used in GetDressedTest to build out the functionality in GetDressed while refactoring supporting modules as needed during the process.

## Usage

Run the GoingToWork console app.

The design document specifies the temperature delimited from other values with a space like this:

Input: HOT 8, 6, 4, 2, 1, 7

Due to time constraints, the app currently expects a comma seperated list of values like this (temperature also separated by comma):

Input: HOT, 8, 6, 4, 2, 1, 7

## TODO

The rules should be updated to allow a list of exluded DressOptions for each rule based on temperature.  Currently there is a //TODO: comment in ExecuteDressCommandsByTemperature() where this logic resides and should be extracted out into the DressRule object.
