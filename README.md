# AspenInfoPlus21TestDemo #
关于Aspen实时数据库API的使用Demo  
AspenInfoPlus.21 API  
***
**数据类型对照**:

|API数据类型|C#对应数据类型|
|----------|------------|
|long word             | int  |
|Integer               | int  |
|byte                  | byte  |
|short word            | short|
|short                 | short|
|character             | short|
|Double precision real | double  |
|single precision real | float|
|Pointer to data       | string  |
|long word array       | `int[]`  |
|short word array      | `short[]` |
|character array       | `byte[]`|
|byte array            | `byte[]`|
|array of addresses    | `object[]`|
|type aligned address  | `object[]`|
|pointer array		   | `string[]`|
|ERRARRAY              | `byte[]`|
|XTSBLOCK			   |XTSBLOCK|
|ERRBLOCK              | ERRBLOCK  |
|IDANDFT               | IDANDFT  |
|FIELDDEFN|未知|

***
**记录类型标识(One)**

|RecordType|Identifier|
|----------|----------|
|Any Record  |RTYPANYRECORD |
|External Task Record  |RTYPEXTASK |
|Field Name Record  |RTYPFLDNAME |
|Definition Record  |RTYPDEFINE |
|Select Descriptor Record  |RTYPSELECT |
|Disk History Record  |RTYPDSKHIST |
|History Summary Line Record  |RTYPHSUMLIN |
|Pseudo Summary Line Record  |RTYPPSUMLIN |
|Normal Summary Line Record  |RTYPNSUMLIN |
|Integer Format Record  |RTYPIFORMAT |
|Real Format Record  |RTYPRFORMAT |
|Timestamp Format Record  |RTYPTFORMAT |
|Detail Display Record  |RTYPDETDSPLY |
|External Task Record  Definition Record  |RTYPDEFEXTASK |
|Field Name Record Definition Record  |RTYPDEFFLDNAME |
|Definition Record Definition Record  |RTYPDEFDEFINE |
|Select Descriptor Record Definition Record |RTYPDEFSELECT |
|Disk History Record Definition Record  |RTYPDEFDSKHIST |
***

**记录类型标识(TWO)**

|RecordType|Identifier|
|----------|----------|
|Any Defined Record  |RTYPANYRECORD |
|External Task Record  |RTYPEXTASK |
|Field Name Record  |RTYPFLDNAME |
|Definition Record  |RTYPDEFINE |
|Select Descriptor Record  |RTYPSELECT |
|Disk History Record  |RTYPDSKHIST |
|History Summary Line Record  |RTYPHSUMLIN |
|Pseudo Summary Line Record  |RTYPPSUMLIN |
|Normal Summary Line Record  |RTYPPSUMLIN |
|Integer Format Record |RTYPIFORMAT |
|Real Format Record  |RTYPRFORMAT |
|Timestamp Format Record  |RTYPTFORMAT |
|Detail Display Record  |RTYPDETDSPLY |
|External Task Record  Definition Record   |RTYPDEFEXTASK |
|Field Name Record Definition Record |RTYPDEFFLDNAME |
|Definition Record Definition Record  |RTYPDEFDEFINE |
|Select Descriptor Record Definition |RTYPDEFSELECT |
|Record | |
|Disk History Record Definition Record  |RTYPDEFDSKHIST |
|Folder Record |RTYPFOLDER |
|Summary Record  |RTYPSUMMARY |
|Integer Validation Processing  |RTYPIVALID |
|Real Validation Processing  |RTYPRVALID |
|Data Compression Processing  |RTYPBOXCAR |
***

**字段的几种写权限**  
* AT_PERMIS_NONE  (no permission) 
* AT_PERMIS_REC_WRITE_GENERAL 
* AT_PERMIS_REC_WRITE_RESTRICTED 
* AT_PERMIS_REC_WRITE_SYSTEM 
***

**error.ERRCODE的几种可用值**  
* NOREC 
* INVEXTSK 
* OPSCGCSI 
* RDSCGCSI 
* RFSCGCSI 
* WRSCGCSI

**关键字**:  
Access Output only   
Mechanism  Passed by reference  
为out
