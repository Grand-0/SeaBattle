In this version Api for run we need:
1) Choose Api directory
2) Initialize dotnet secrets storage with command: dotnet user-secrets init;
3) Create new secret key with name TokenKey and a value divisible by 16 with command: 
dotnet user-secrets set "TokenKey" "myTokenKey"