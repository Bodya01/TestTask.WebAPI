# Test task for bART Solutions

## How to run this on your device

Clone repository by using:

```bash
  git clone https://github.com/Bodya01/TestTask.WebAPI.git
```

Put you connection string in appsettings.Development.json:

```txt
  "DbConnection": "Put your conncetion string here" 
```
Run Update-Database command in Package Manager Console:

```bash
  Update-Database
```

## This API is using Swagger to make work with it easier.

## Usage rules

```1) Account cannot be created without contact.```

```2) Incident cannot be created without account.```

```3) After creating an incident or account you can update all existing records or add new ones.```

```4) Post methods are used to create a record, Put to update them.```

## Thank you for attention :)
