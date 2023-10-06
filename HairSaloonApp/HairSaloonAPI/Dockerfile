#Base Image
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

#Create & Use /src
WORKDIR /src

#Copy dependencies & install if changed or first
COPY *.csproj ./
RUN dotnet restore

#Copy all files
COPY . ./

#Build & Publish
RUN dotnet build -o /app
RUN dotnet publish -o /publish

#Change to published folder
WORKDIR /publish

#Expose port 7035 on host machine
EXPOSE 7035

#Start App
ENTRYPOINT ["./HairSaloonAPI"]