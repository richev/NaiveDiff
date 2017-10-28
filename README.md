# NaiveDiff
A pretty naive diffing utility.

Let's say I have two log files, let's say they're from my CI build. I want to know the *interesting* differences, ignoring all of the date time stamps, file paths and who knows what else that differs between the files.

This project aims to solve that problem.

**Assumption:** You can figure out a regular expression that defines each of the differences you don't care about.
