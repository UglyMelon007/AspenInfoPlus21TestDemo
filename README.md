# AspenInfoPlus21TestDemo #
关于Aspen实时数据库API的使用Demo  
AspenInfoPlus.21 API  
***
**数据类型对照**:

|API数据类型|对应数据类型|
|----------|------------|
|long word             | int  |
|Integer               | int  |
|byte                  | byte  |
|short word            | short|
|short                 | short|
|Double precision real | double  |
|single precision real | float|
|Pointer to data       | string  |
|long word array       | `int[]`  |
|short word array      | `short[]` |
|character array       | `byte[]`|
|byte array            | `byte[]`|
|XTSBLOCK			   |XTSBLOCK|
|ERRARRAY              | `byte[]`|
|ERRBLOCK              | ERRBLOCK  |
|IDANDFT               | IDANDFT  (不准）|
|FIELDDEFN|未知|

***
**记录类型标识**

|RecordType|Identifier|
|----------|----------|
|lAny Record  |RTYPANYRECORD |
|lExternal Task Record  |RTYPEXTASK |
|lField Name Record  |RTYPFLDNAME |
|lDefinition Record  |RTYPDEFINE |
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
