﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Security
{
    enum PermissionCategories
    {
        Persons = 1,
        Web = 2,
        Accounts = 3,
        Request = 4,
        RequestDevice = 5,
        Warehouse = 6,
        BasicInformation = 7,
        Person_RealPerson = 8,
        Person_Company = 9,
        Person_Warehouse = 10,
        Person_Bank = 11,
        RequestDevice_All = 12,
        RequestDevice_Validation = 13,
        RequestDevice_ProjectAssignment = 14,
        RequestDevice_SendToSwitch = 15,
        RequestDevice_Install = 16,
        RequestDevice_Cancel = 17,
        BasicInformation_Main = 18,
        BasicInformation_Base = 19,
        Documanes = 20,
        Stuff = 21,
        Pos = 22,
        //Warehouse = 23,
        Guarantee = 24,
        Insurance = 25,
        StuffType = 26,
        DeviceModel = 27,
        CountUnit = 28,
        Quality = 29,
        DocumentType = 30,
        DocumentStatus = 31,
        Document_Increase = 32,
        Document_Decrease = 33,
        BasicInformation_Person = 34,
        BasicInformation_Address = 35,
        BasicInformation_Pay = 36,
        BasicInformation_Users = 37,
        BasicInformation_MerchantCatagury = 38,
        BasicInformation_Job = 39,
        BasicInformation_CompanyType = 40,
        BasicInformation_OwnershipType = 41,
        BasicInformation_EducationLevel = 42,
        BasicInformation_EducationMajor = 43,
        BasicInformation_Gender = 44,
        BasicInformation_PersonType = 45,
        BasicInformation_MaritalStatus = 46,
        BasicInformation_GeographicArea = 47,
        BasicInformation_AddressCategory = 48,
        BasicInformation_AddressElementPrefix = 49,
        BasicInformation_PlaceType = 50,
        BasicInformation_Place = 51,
        BasicInformation_Project = 52,
        BasicInformation_DeviceModel = 53,
        BasicInformation_Facility = 54,
        BasicInformation_Switch = 55,
        BasicInformation_AccountType = 56,
        BasicInformation_BeneficiaryGroup = 57,
        BasicInformation_CancelReason = 58,
        BasicInformation_Platform = 59,
        BasicInformation_StatusType = 60,
        BasicInformation_TrustType = 61,
        BasicInformation_System = 62,
        BasicInformation_User = 63,
        Cartable = 65,
        Task = 66,
        Priority = 67,
        TaskStatus = 68,
        TaskType = 69
    }

    enum Permissions
    {
        Web_Create = 13,
        Web_Modify = 14,
        Web_Delete = 15,
        Web_View = 143,
        Account_Create = 16,
        Account_Modify = 17,
        Account_Delete = 18,
        Account_View = 212,
        Request_Create = 19,
        Request_Modify = 20,
        Request_Delete = 21,
        Request_View = 122,
        RealPerson_Create = 1,
        RealPerson_Modify = 2,
        RealPerson_Delete = 3,
        RealPerson_View = 127,
        Company_Create = 4,
        Company_Modify = 5,
        Company_Delete = 6,
        Company_View = 126,
        Warehouse_Create = 7,
        Warehouse_Modify = 8,
        Warehouse_Delete = 9,
        Warehouse_View = 129,
        Bank_Create = 10,
        Bank_Modify = 11,
        Bank_Delete = 12,
        Bank_View = 115,
        RequestDevice_Modify = 1202,
        RequestDevice_All_View = 157,
        RequestDevice_MyAll_View = 161,
        RequestDevice_Validation_View = 134,
        RequestDevice_Validation_ALL_View = 1203,
        RequestDevice_Validation = 22,
        RequestDevice_ProjectAssign = 23,
        RequestDevice_ProjectAssign_View = 135,
        RequestDevice_ProjectAssign_All_View = 159,
        RequestDevice_SendToSwitch = 164,
        RequestDevice_SendToSwitch_View = 166,
        RequestDevice_SendToSwitch_All_View = 163,
        RequestDevice_SendToSwitch_Install_View = 137,
        RequestDevice_DeviceAssign = 1207,
        RequestDevice_DeviceAssign_View = 1206,
        RequestDevice_DeviceAssign_All_View = 1205,
        RequestDevice_Install_View = 1210,
        RequestDevice_Install_All_View = 1209,
        RequestDevice_Install = 24,
        RequestDevice_Cancel = 25,
        RequestDevice_SendToSwitch_Cancel_All_View = 136,
        RequestDevice_SendToSwitch_Cancel_View = 136,
        Stuff_Create = 82,
        Stuff_Modify = 83,
        Stuff_Delete = 84,
        Stuff_View = 131,
        Pos_Create = 88,
        Pos_Modify = 89,
        Pos_Delete = 90,
        Pos_View = 167,
        Guarantee_Create = 91,
        Guarantee_Modify = 92,
        Guarantee_Delete = 93,
        Guarantee_View = 132,
        Insurance_Create = 94,
        Insurance_Modify = 95,
        Insurance_Delete = 96,
        Insurance_View = 168,
        StuffType_Create = 85,
        StuffType_Modify = 86,
        StuffType_Delete = 87,
        StuffType_View = 112,
        DeviceModel_Create = 173,
        DeviceModel_Modify = 174,
        DeviceModel_Delete = 175,
        DeviceModel_View = 176,
        CountUnit_Create = 97,
        CountUnit_Modify = 98,
        CountUnit_Delete = 99,
        CountUnit_View = 142,
        Quality_Create = 100,
        Quality_Modify = 101,
        Quality_Delete = 102,
        Quality_View = 113,
        DocumentType_Create = 177,
        DocumentType_Modify = 178,
        DocumentType_Delete = 179,
        DocumentType_View = 180,
        DocumentStatus_Create = 181,
        DocumentStatus_Modify = 182,
        DocumentStatus_Delete = 183,
        DocumentStatus_View = 184,
        Document_Create = 185,
        Document_Modify = 186,
        Document_Delete = 187,
        Document_View = 188,
        MerchantCategory_Create = 26,
        MerchantCategory_Modify = 27,
        MerchantCategory_Delete = 28,
        MerchantCategory_View = 105,
        MerchantCategory_Mapping = 147,
        Job_Create = 29,
        Job_Modify = 30,
        Job_Delete = 31,
        Job_View = 138,
        CompanyType_Create = 32,
        CompanyType_Modify = 33,
        CompanyType_Delete = 34,
        CompanyType_View = 111,
        OwnershipType_Create = 35,
        OwnershipType_Modify = 36,
        OwnershipType_Delete = 37,
        OwnershipType_View = 114,
        EducationLevel_Create = 38,
        EducationLevel_Modify = 39,
        EducationLevel_Delete = 40,
        EducationLevel_View = 140,
        EducationMajor_Create = 41,
        EducationMajor_Modify = 42,
        EducationMajor_Delete = 43,
        EducationMajor_View = 124,
        Gender_Modify = 45,
        Gender_View = 120,
        Gender_Mapping = 151,
        PersonType_Modify = 44,
        PersonType_View = 107,
        PersonType_Mapping = 148,
        MaritalStatus_Modify = 146,
        MaritalStatus_View = 144,
        MaritalStatus_Mapping = 154,
        GeographicArea_Create = 46,
        GeographicArea_Modify = 47,
        GeographicArea_Delete = 48,
        GeographicArea_View = 141,
        GeographicArea_Mapping = 153,
        AddressCategory_Create = 49,
        AddressCategory_Modify = 50,
        AddressCategory_Delete = 51,
        AddressCategory_View = 123,
        AddressElementPrefix_Create = 52,
        AddressElementPrefix_Modify = 53,
        AddressElementPrefix_Delete = 54,
        AddressElementPrefix_View = 119,
        PlaceType_Create = 55,
        PlaceType_Modify = 56,
        PlaceType_Delete = 57,
        PlaceType_View = 106,
        Place_Create = 58,
        Place_Modify = 59,
        Place_Delete = 60,
        Place_View = 139,
        Project_Create = 61,
        Project_Modify = 62,
        Project_Delete = 63,
        Project_View = 118,
        DeviceType_Create = 64,
        DeviceType_Modify = 65,
        DeviceType_Delete = 66,
        DeviceType_View = 108,
        DeviceType_Mapping = 149,
        Facility_Create = 70,
        Facility_Modify = 71,
        Facility_Delete = 72,
        Facility_View = 130,
        Switch_Create = 73,
        Switch_Modify = 74,
        Switch_Delete = 75,
        Switch_View = 125,
        AccountType_Create = 76,
        AccountType_Modify = 77,
        AccountType_Delete = 78,
        AccountType_View = 110,
        AccountType_Mapping = 150,
        BeneficiaryGroup_Create = 79,
        BeneficiaryGroup_Modify = 80,
        BeneficiaryGroup_Delete = 81,
        BeneficiaryGroup_View = 133,
        CancelReason_Create = 103,
        CancelReason_Modify = 145,
        CancelReason_Delete = 104,
        CancelReason_View = 128,
        CancelReason_Mapping = 152,
        Platform_Create = 193,
        Platform_Modify = 194,
        Platform_Delete = 195,
        Platform_View = 196,
        StatusType_Modify = 197,
        StatusType_View = 198,
        TrustType_Modify = 200,
        TrustType_View = 203,
        System_Create = 204,
        System_Modify = 205,
        System_Delete = 206,
        System_View = 207,
        User_Create = 208,
        User_Modify = 209,
        User_Delete = 210,
        User_View = 211,
        Task_Create = 1212,
        Task_Modify = 1213,
        Task_Delete = 1214,
        Task_View = 1211,
        Priority_Create = 1216,
        Priority_Modify = 1217,
        Priority_Delete = 1218,
        Priority_View = 1215,
        TaskStatus_Create = 1220,
        TaskStatus_Modify = 1221,
        TaskStatus_Delete = 1222,
        TaskStatus_View = 1219,
        TaskType_Create = 1224,
        TaskType_Modify = 1225,
        TaskType_Delete = 1226,
        TaskType_View = 1223,
        DeviceChangeReason_Create = 1227,
        DeviceChangeReason_Modify = 1228,
        DeviceChangeReason_Delete = 1229,
        DeviceChangeReason_View = 1230,
        InformationSystem_Create = 1231,
        InformationSystem_Modify = 1232,
        InformationSystem_Delete = 1233,
        InformationSystem_View = 1234,
        PosOwnershipType_Create = 1235,
        PosOwnershipType_Modify = 1236,
        PosOwnershipType_Delete = 1237,
        PosOwnershipType_View = 1238,
        AccountWithRequestDevice_Modify = 1240,
        RequestDeviceAccount_Modify = 1242
    }
}