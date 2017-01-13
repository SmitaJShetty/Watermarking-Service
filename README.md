# Watermarking-Service

A simple watermarking application developed using WCF and iTextSharp on C#.
Service api receives file content in byte array and text string that has to be watermarked on the pdf file. The watermarked pdf file is the output of the api.
Project contains a client console application. 
MSTest is used for unittesting with moq. 
Ninject extension for WCF has been used for injecting service references. The utility assembly uses itextsharp now. 
A different implementation can be bound to service for watermarking if needed. The itextsharp implementation is bound using ninject as well. 
