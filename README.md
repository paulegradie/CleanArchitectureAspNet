# Clean Architecture AspNet

Welcome to your study companion for understanding 'Clean Architecture' principles described in the book [Clean Architecture: A Craftsman's Guide to Software Structure and Design]() by Robert C. Martin.

As part of my study of this this incredibly useful resource, I've developed a relatively basic starter web server in C# (asp.net, .net8). This server comes in two parts:

1. An implementation that represents my understanding of how to architect BEFORE reading the book
2. An updated implementation (with no - or minor if I don't recall correctly - additions to the code base) that represents my understand AFTER reading the book.

I took my time with this book. In it you will find familiar concepts, but potentially completely unfamiliar approachs to software design that may not only be counter-intuitive, but also challenge the very core of your understanding of how to architect software.


## Server contents

This webapp uses Microsoft Identity, models organizations, admins, and organization members, and includes an integration and performance test project (using Sailfish) and a Client.

## Purpose

The purpose of this repository is two-fold:

1. Use it to grasp a deeper understanding of good architecture and identify in your own codebases (or those orgs you contribute to) the different layers of your application, and how to compose them in a way that is maintainable and testable.

2. Use the finished product as a launch pad for your own application! If you start with this design (and in particular if you can come to _understand_ the design) - your application will be off to a good start.


## How to use the repository

### For study

First, I suggest you read the book! Again, its a great source of knowledge for those interersted in making informed architectural decisions. Next, attemp (as many times as you need!) to refactor the codebase into components that follow SOLID and clean architecture principles. Finally, compare your finished result to mine, compare, and contrast!

Treat this as an exercise in the same way as you would any other, e.g. [The Guilded Kata]().

### As a starter server

Feel free to fork or take the code - just be sure to use the version that follows 'Clean Architecture' principles. ;)