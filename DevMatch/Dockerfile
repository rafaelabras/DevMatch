FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . .


RUN ["dotnet", "tool", "install", "--global" ,"dotnet-ef"]
ENV DOTNET_TOOLS="/root/.dotnet/tools"
ENV PATH="$PATH:${DOTNET_TOOLS}"
RUN ["sleep", "20"]