﻿using Io.HcxProtocol.Dto;
using Io.HcxProtocol.Exceptions;
using Io.HcxProtocol.Impl;
using Io.HcxProtocol.Init;
using Io.HcxProtocol.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest.Impl
{
    [TestClass]
    public class HCXIncomingOutgoingRequestTest
    {
        private void InitializeConfig()
        {
            Config configObj = new Config();

            configObj.ProtocolBasePath = "http://dev-hcx.swasth.app/api/v0.7";
            configObj.ParticipantCode = "testprovider1.apollo@swasth-hcx-dev";
            configObj.AuthBasePath = "http://aef8101c432d04a659e6317b3e5031cf-1674586068.ap-south-1.elb.amazonaws.com:8080/auth/realms/swasth-health-claim-exchange/protocol/openid-connect/token";
            configObj.UserName = "testprovider1@apollo.com";
            configObj.Password = "Opensaber@123";
            configObj.EncryptionPrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCG+XLPYiCxrZq71IX+w7uoDGxGI7qy7XaDbL3BJE33ju7rjdrP7wsAOWRvM8BIyWuRZZhl9xG+u7l/7OsZAzGoqI7p+32x+r9IJVzboLDajk6tp/NPg1csc7f2M5Bu6rkLEvrKLz3dgy3Q928rMsD3rSmzBLelfKTo+aDXvCOiw1dMWsZZdkEpCTJxH39Nb2K4S59kO/R2GtSU/QMLq65m34XcMZpDtatA1u1S8JdZNNeMCO+NuFKBzIfvXUCQ8jkf7h612+UP1AYhoyCMFpzUZ9b7liQF9TYpX1Myr/tT75WKuRlkFlcALUrtVskL8KA0w6sA0nX5fORVsuVehVeDAgMBAAECggEAX1n1y5/M7PhxqWO3zYTFGzC7hMlU6XZsFOhLHRjio5KsImgyPlbm9J+W3iA3JLR2c17MTKxAMvg3UbIzW5YwDLAXViC+aW90like8mEQzzVdS7ysXG2ytcqCGUHQNStI0hP0a8T39XbodQl31ZKjU9VW8grRGe12Kse+4ukcW6yRVES+CkyO5BQB+vs3voZavodRGsk/YSt00PtIrFPJgkDuyzzcybKJD9zeJk5W3OGVK1z0on+NXKekRti5FBx/uEkT3+knkz7ZlTDNcyexyeiv7zSL/L6tcszV0Fe0g9vJktqnenEyh4BgbqABPzQR++DaCgW5zsFiQuD0hMadoQKBgQC+rekgpBHsPnbjQ2Ptog9cFzGY6LRGXxVcY7hKBtAZOKAKus5RmMi7Uv7aYJgtX2jt6QJMuE90JLEgdO2vxYG5V7H6Tx+HqH7ftCGZq70A9jFBaba04QAp0r4TnD6v/LM+PGVT8FKtggp+o7gZqXYlSVFm6YzI37G08w43t2j2aQKBgQC1Nluxop8w6pmHxabaFXYomNckziBNMML5GjXW6b0xrzlnZo0p0lTuDtUy2xjaRWRYxb/1lu//LIrWqSGtzu+1mdmV2RbOd26PArKw0pYpXhKFu/W7r6n64/iCisoMJGWSRJVK9X3D4AjPaWOtE+jUTBLOk0lqPJP8K6yiCA6ZCwKBgDLtgDaXm7HdfSN1/Fqbzj5qc3TDsmKZQrtKZw5eg3Y5CYXUHwbsJ7DgmfD5m6uCsCPa+CJFl/MNWcGxeUpZFizKn16bg3BYMIrPMao5lGGNX9p4wbPN5J1HDD1wnc2jULxupSGmLm7pLKRmVeWEvWl4C6XQ+ykrlesef82hzwcBAoGBAKGY3v4y4jlSDCXaqadzWhJr8ffdZUrQwB46NGb5vADxnIRMHHh+G8TLL26RmcET/p93gW518oGg7BLvcpw3nOZaU4HgvQjT0qDvrAApW0V6oZPnAQUlarTU1Uk8kV9wma9tP6E/+K5TPCgSeJPg3FFtoZvcFq0JZoKLRACepL3vAoGAMAUHmNHvDI+v0eyQjQxlmeAscuW0KVAQQR3OdwEwTwdFhp9Il7/mslN1DLBddhj6WtVKLXu85RIGY8I2NhMXLFMgl+q+mvKMFmcTLSJb5bJHyMz/foenGA/3Yl50h9dJRFItApGuEJo/30cG+VmYo2rjtEifktX4mDfbgLsNwsI=\n-----END PRIVATE KEY-----";
            configObj.IgUrl = "test";

            HCXIntegrator.initConfig(configObj);
        }

        string commonFhirPayload = "{ \"resourceType\": \"Bundle\", \"id\": \"d4484cdd-1aae-4d21-a92e-8ef749d6d366\", \"meta\": { \"lastUpdated\": \"2022-02-08T21:49:55.458+05:30\" }, \"identifier\": { \"system\": \"https://www.tmh.in/bundle\", \"value\": \"d4484cdd-1aae-4d21-a92e-8ef749d6d366\" }, \"type\": \"document\", \"timestamp\": \"2022-02-08T21:49:55.458+05:30\", \"entry\": [{ \"fullUrl\": \"Composition/42ff4a07-3e36-402f-a99e-29f16c0c9eee\", \"resource\": { \"resourceType\": \"Composition\", \"id\": \"42ff4a07-3e36-402f-a99e-29f16c0c9eee\", \"identifier\": { \"system\": \"https://www.tmh.in/hcx-documents\", \"value\": \"42ff4a07-3e36-402f-a99e-29f16c0c9eee\" }, \"status\": \"final\", \"type\": { \"coding\": [{ \"system\": \"https://www.hcx.org/document-type\", \"code\": \"HcxCoverageEligibilityRequest\", \"display\": \"Coverage Eligibility Request Doc\" }] }, \"subject\": { \"reference\": \"Patient/RVH1003\" }, \"date\": \"2022-02-08T21:49:55+05:30\", \"author\": [{ \"reference\": \"Organization/Tmh01\" }], \"title\": \"Coverage Eligibility Request\", \"section\": [{ \"title\": \"# Eligibility Request\", \"code\": { \"coding\": [{ \"system\": \"https://fhir.loinc.org/CodeSystem/$lookup?system=http://loinc.org&code=10154-3\", \"code\": \"CoverageEligibilityRequest\", \"display\": \"Coverage Eligibility Request\" }] }, \"entry\": [{ \"reference\": \"CoverageEligibilityRequest/dc82673b-8c71-48c2-8a17-16dcb3b035f6\" }] }] } }, { \"fullUrl\": \"Organization/Tmh01\", \"resource\": { \"resourceType\": \"Organization\", \"id\": \"Tmh01\", \"identifier\": [{ \"system\": \"http://abdm.gov.in/facilities\", \"value\": \"HFR-ID-FOR-TMH\" }, { \"system\": \"http://irdai.gov.in/facilities\", \"value\": \"IRDA-ID-FOR-TMH\" } ], \"name\": \"Tata Memorial Hospital\", \"alias\": [ \"TMH\", \"TMC\" ], \"telecom\": [{ \"system\": \"phone\", \"value\": \"(+91) 022-2417-7000\" }], \"address\": [{ \"line\": [ \"Dr Ernest Borges Rd, Parel East, Parel, Mumbai, Maharashtra 400012\" ], \"city\": \"Mumbai\", \"state\": \"Maharashtra\", \"postalCode\": \"400012\", \"country\": \"INDIA\" }], \"endpoint\": [{ \"reference\": \"https://www.tmc.gov.in/\", \"display\": \"Website\" }] } }, { \"fullUrl\": \"Patient/RVH1003\", \"resource\": { \"resourceType\": \"Patient\", \"id\": \"RVH1003\", \"identifier\": [{ \"type\": { \"coding\": [{ \"system\": \"http://terminology.hl7.org/CodeSystem/v2-0203\", \"code\": \"SN\", \"display\": \"Subscriber Number\" }] }, \"system\": \"http://gicofIndia.com/beneficiaries\", \"value\": \"BEN-101\" }, { \"system\": \"http://abdm.gov.in/patients\", \"value\": \"hinapatel@abdm\" } ], \"name\": [{ \"text\": \"Hina Patel\" }], \"gender\": \"female\" } }, { \"fullUrl\": \"CoverageEligibilityRequest/dc82673b-8c71-48c2-8a17-16dcb3b035f6\", \"resource\": { \"resourceType\": \"CoverageEligibilityRequest\", \"id\": \"dc82673b-8c71-48c2-8a17-16dcb3b035f6\", \"identifier\": [{ \"system\": \"https://www.tmh.in/coverage-eligibility-request\", \"value\": \"dc82673b-8c71-48c2-8a17-16dcb3b035f6\" }], \"status\": \"active\", \"purpose\": [ \"discovery\" ], \"patient\": { \"reference\": \"Patient/RVH1003\" }, \"servicedPeriod\": { \"start\": \"2022-02-07T21:49:56+05:30\", \"end\": \"2022-02-09T21:49:56+05:30\" }, \"created\": \"2022-02-08T21:49:56+05:30\", \"provider\": { \"reference\": \"Organization/Tmh01\" }, \"insurer\": { \"reference\": \"Organization/GICOFINDIA\" }, \"insurance\": [{ \"focal\": true, \"coverage\": { \"reference\": \"Coverage/dadde132-ad64-4d18-8c18-1d52d7e86abc\" } }] } }, { \"fullUrl\": \"Organization/GICOFINDIA\", \"resource\": { \"resourceType\": \"Organization\", \"id\": \"GICOFINDIA\", \"identifier\": [{ \"system\": \"http://irdai.gov.in/insurers\", \"value\": \"112\" }], \"name\": \"General Insurance Corporation of India\" } }, { \"fullUrl\": \"Coverage/dadde132-ad64-4d18-8c18-1d52d7e86abc\", \"resource\": { \"resourceType\": \"Coverage\", \"id\": \"dadde132-ad64-4d18-8c18-1d52d7e86abc\", \"identifier\": [{ \"system\": \"https://www.gicofIndia.in/policies\", \"value\": \"policy-RVH1003\" }], \"status\": \"active\", \"subscriber\": { \"reference\": \"Patient/RVH1003\" }, \"subscriberId\": \"SN-RVH1003\", \"beneficiary\": { \"reference\": \"Patient/RVH1003\" }, \"relationship\": { \"coding\": [{ \"system\": \"http://terminology.hl7.org/CodeSystem/subscriber-relationship\", \"code\": \"self\" }] }, \"payor\": [{ \"reference\": \"Organization/GICOFINDIA\" }] } } ] }";

        [TestMethod] //(1)
        public void HcxOutgoingRequestCheckSucess()
        {
            InitializeConfig();
            HCXOutgoingRequest hcxOutgoingRequest = new HCXOutgoingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            hcxOutgoingRequest.Generate(commonFhirPayload, Operations.COVERAGE_ELIGIBILITY_CHECK, "testprovider1.apollo@swasth-hcx-dev", output);
            BaseRequest baseRequest = new BaseRequest(output);
            Assert.AreEqual(HCXIntegrator.config.ParticipantCode, baseRequest.GetHcxSenderCode());

            //Note Output :   [Same in JAVA SDK]
            //Output : ERR_INVALID_SENDER_AND_RECIPIENT -- Sender and recipient code cannot be the same
        }

        [TestMethod] //(2)
        public void HcxOutgoingRequestOncheckSucess()
        {
            InitializeConfig();
            HCXOutgoingRequest hcxOutgoingRequest = new HCXOutgoingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            hcxOutgoingRequest.Generate(commonFhirPayload, Operations.COVERAGE_ELIGIBILITY_ON_CHECK, "testprovider1.apollo@swasth-hcx-dev", output);
            BaseRequest baseRequest = new BaseRequest(output);
            Assert.AreEqual(HCXIntegrator.config.ParticipantCode, baseRequest.GetHcxSenderCode());

            //Note Output :   [Same in JAVA SDK]
            //Output : "ERR_ACCESS_DENIED","message":"Does not have access to the called API"
        }

        [TestMethod] //(3)
        public void HcxOutgoingRequestResourceTypeError()
        {
            InitializeConfig();
            HCXOutgoingRequest hcxOutgoingRequest = new HCXOutgoingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            string fhirPayload = "{\"id\": \"d4484cdd-1aae-4d21-a92e-8ef749d6d366\", \"meta\": { \"lastUpdated\": \"2022-02-08T21:49:55.458+05:30\" }, \"identifier\": { \"system\": \"https://www.tmh.in/bundle\", \"value\": \"d4484cdd-1aae-4d21-a92e-8ef749d6d366\" }, \"type\": \"document\", \"timestamp\": \"2022-02-08T21:49:55.458+05:30\", \"entry\": [{ \"fullUrl\": \"Composition/42ff4a07-3e36-402f-a99e-29f16c0c9eee\", \"resource\": { \"resourceType\": \"Composition\", \"id\": \"42ff4a07-3e36-402f-a99e-29f16c0c9eee\", \"identifier\": { \"system\": \"https://www.tmh.in/hcx-documents\", \"value\": \"42ff4a07-3e36-402f-a99e-29f16c0c9eee\" }, \"status\": \"final\", \"type\": { \"coding\": [{ \"system\": \"https://www.hcx.org/document-type\", \"code\": \"HcxCoverageEligibilityRequest\", \"display\": \"Coverage Eligibility Request Doc\" }] }, \"subject\": { \"reference\": \"Patient/RVH1003\" }, \"date\": \"2022-02-08T21:49:55+05:30\", \"author\": [{ \"reference\": \"Organization/Tmh01\" }], \"title\": \"Coverage Eligibility Request\", \"section\": [{ \"title\": \"# Eligibility Request\", \"code\": { \"coding\": [{ \"system\": \"https://fhir.loinc.org/CodeSystem/$lookup?system=http://loinc.org&code=10154-3\", \"code\": \"CoverageEligibilityRequest\", \"display\": \"Coverage Eligibility Request\" }] }, \"entry\": [{ \"reference\": \"CoverageEligibilityRequest/dc82673b-8c71-48c2-8a17-16dcb3b035f6\" }] }] } }, { \"fullUrl\": \"Organization/Tmh01\", \"resource\": { \"resourceType\": \"Organization\", \"id\": \"Tmh01\", \"identifier\": [{ \"system\": \"http://abdm.gov.in/facilities\", \"value\": \"HFR-ID-FOR-TMH\" }, { \"system\": \"http://irdai.gov.in/facilities\", \"value\": \"IRDA-ID-FOR-TMH\" } ], \"name\": \"Tata Memorial Hospital\", \"alias\": [ \"TMH\", \"TMC\" ], \"telecom\": [{ \"system\": \"phone\", \"value\": \"(+91) 022-2417-7000\" }], \"address\": [{ \"line\": [ \"Dr Ernest Borges Rd, Parel East, Parel, Mumbai, Maharashtra 400012\" ], \"city\": \"Mumbai\", \"state\": \"Maharashtra\", \"postalCode\": \"400012\", \"country\": \"INDIA\" }], \"endpoint\": [{ \"reference\": \"https://www.tmc.gov.in/\", \"display\": \"Website\" }] } }, { \"fullUrl\": \"Patient/RVH1003\", \"resource\": { \"resourceType\": \"Patient\", \"id\": \"RVH1003\", \"identifier\": [{ \"type\": { \"coding\": [{ \"system\": \"http://terminology.hl7.org/CodeSystem/v2-0203\", \"code\": \"SN\", \"display\": \"Subscriber Number\" }] }, \"system\": \"http://gicofIndia.com/beneficiaries\", \"value\": \"BEN-101\" }, { \"system\": \"http://abdm.gov.in/patients\", \"value\": \"hinapatel@abdm\" } ], \"name\": [{ \"text\": \"Hina Patel\" }], \"gender\": \"female\" } }, { \"fullUrl\": \"CoverageEligibilityRequest/dc82673b-8c71-48c2-8a17-16dcb3b035f6\", \"resource\": { \"resourceType\": \"CoverageEligibilityRequest\", \"id\": \"dc82673b-8c71-48c2-8a17-16dcb3b035f6\", \"identifier\": [{ \"system\": \"https://www.tmh.in/coverage-eligibility-request\", \"value\": \"dc82673b-8c71-48c2-8a17-16dcb3b035f6\" }], \"status\": \"active\", \"purpose\": [ \"discovery\" ], \"patient\": { \"reference\": \"Patient/RVH1003\" }, \"servicedPeriod\": { \"start\": \"2022-02-07T21:49:56+05:30\", \"end\": \"2022-02-09T21:49:56+05:30\" }, \"created\": \"2022-02-08T21:49:56+05:30\", \"provider\": { \"reference\": \"Organization/Tmh01\" }, \"insurer\": { \"reference\": \"Organization/GICOFINDIA\" }, \"insurance\": [{ \"focal\": true, \"coverage\": { \"reference\": \"Coverage/dadde132-ad64-4d18-8c18-1d52d7e86abc\" } }] } }, { \"fullUrl\": \"Organization/GICOFINDIA\", \"resource\": { \"resourceType\": \"Organization\", \"id\": \"GICOFINDIA\", \"identifier\": [{ \"system\": \"http://irdai.gov.in/insurers\", \"value\": \"112\" }], \"name\": \"General Insurance Corporation of India\" } }, { \"fullUrl\": \"Coverage/dadde132-ad64-4d18-8c18-1d52d7e86abc\", \"resource\": { \"resourceType\": \"Coverage\", \"id\": \"dadde132-ad64-4d18-8c18-1d52d7e86abc\", \"identifier\": [{ \"system\": \"https://www.gicofIndia.in/policies\", \"value\": \"policy-RVH1003\" }], \"status\": \"active\", \"subscriber\": { \"reference\": \"Patient/RVH1003\" }, \"subscriberId\": \"SN-RVH1003\", \"beneficiary\": { \"reference\": \"Patient/RVH1003\" }, \"relationship\": { \"coding\": [{ \"system\": \"http://terminology.hl7.org/CodeSystem/subscriber-relationship\", \"code\": \"self\" }] }, \"payor\": [{ \"reference\": \"Organization/GICOFINDIA\" }] } } ] }";
            hcxOutgoingRequest.Generate(fhirPayload, Operations.COVERAGE_ELIGIBILITY_CHECK, "testprovider1.apollo@swasth-hcx-dev", output);
            Assert.IsTrue(output.ContainsKey(ErrorCodes.ERR_WRONG_DOMAIN_PAYLOAD.ToString()));

            //Note Output :   [Same in JAVA SDK]
            //Output : ERR_WRONG_DOMAIN_PAYLOAD - Invalid payload is not accepted
        }

        [TestMethod] //(4)
        public void HcxOutgoingErrorDetails()
        {
            InitializeConfig();
            HCXOutgoingRequest hcxOutgoingRequest = new HCXOutgoingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            string errorDetails = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1lcnJvcl9kZXRhaWxzIjp7CiJjb2RlIjoiRVJSX0lOVkFMSURfUEFZTE9BRCIsCiJtZXNzYWdlIjoiaW52YWxpZCByZXF1ZXN0IHBheWxvYWQiLAoidHJhY2UiOiIifX0";
            hcxOutgoingRequest.Generate(commonFhirPayload, Operations.COVERAGE_ELIGIBILITY_CHECK, errorDetails, "response.complete", output);

            Assert.IsFalse(errorDetails.IsNullOrEmpty());

            //Note Output :   [Same in JAVA SDK]
            //Output : The given key was not present in the dictionary.
            // (Note-This due to No Participants Return by API call for Participant PublicKey)
        }

        [TestMethod] //(5)
        public void HcxIncomingRequestSuccess()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string outputPayload = "eyJ4LWhjeC1yZWNpcGllbnRfY29kZSI6InRlc3Rwcm92aWRlcjEuYXBvbGxvQHN3YXN0aC1oY3gtZGV2IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0xMC0yN1QxMTowNzo0OCswNTMwIiwieC1oY3gtc2VuZGVyX2NvZGUiOiJ0ZXN0cHJvdmlkZXIxLmFwb2xsb0Bzd2FzdGgtaGN4LWRldiIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiZDRmNTZkNzktNDkwOC00YTk5LWE4ZGQtYTNiNzMzZmRlOGQ2IiwiZW5jIjoiQTI1NkdDTSIsImFsZyI6IlJTQS1PQUVQLTI1NiIsIngtaGN4LWFwaV9jYWxsX2lkIjoiMWUxNzk3YmQtZGJlZC00MTkyLWIwYzktY2VmNzcyNzI0YmU1In0.NSYks0P3BizbgpGF7GctpBSFDfSfap2V7AnZ5YCQMy_V0F6IZ1weRbZrBLHDTnPwPOBGGfctXpyqiXfvldMjCI_maakNjagsyC2x0pFC6NGmYhTwjqWmpL2CCaneBf9HikqwuI2cJTK8-DNOkbT9Qj8j-NxyGv1NX8UFI1K90t9e61qJ_Xurp6Qrrt6X_fiW7Jx9Vm54kCS7ZUfGK2rw_EOvc1VydsdWnUABOcmbtcTJiSVecNdRYxiKAIsiZCHULdd92a3hzbqFbyfRu1GqmuEyoimjd7jvdDSuB4bhE6WCIF-wB9Z5d3mj1ZXS8AEGT9YuSFLQTyPfo9Di3-Px-Q.0MliuZkDRADgQXY5.LWL2_vhMA8BEYtmg8SFXVoDNWvwWLZcJ9KxOHR7VWdIRxsO7PfNk7yMhctRtmAnyaaNZYZb0e3nVTai-u9aSixhmbxq6rFe3an0HbPi43BIT4ytal-CRxWoTaoBMZLmNqSr7GAvAXxNxtkSpDRUMlGg-tGdrHmcW6b1KAbA2CxE62XlaMMt5DNarXMSBDMx1_Pz0x9QqwFY-8O1GOIwPVZF59fh2e8ytlsvTh1fgiMZSOLETzN5pgCWGacc1D-tSmDD5kDQZm3SPgwZMbkco2xmKdvfdZa1AX7rDrYO6Pl-WFxv_5SO1CWwtR45tsCCp4pIOv3VUIjwv2YlmoTgXXg-EoLNgA4N8sw4kXLNtV9hMys2JdyQ93quEcFGnPO9jMg9seAT3pq_llpkN1FqE2x7zwWFeAs0g48IFc-39td6kdgOnuVEVsGAyE_OEst8TAhvRBa7EOeZmZpM1fiANdLor2xKWOI2K3o52R2XOYnchOWwjF07eWOPYulvSZQPBc1LZ_1j1myJz4qNH6aKWxkFT99FBddQ_UA49SgHRhYNScs27Ycw0BEZso9UpiXoLDJUKIp0Q717BkM5K1DdwYixkIMSEAM2HZHRaD0fk_8C0Xx0C1WxpZAhOtjKkesd1O2iUA2YbOyClgdASSUjO9KWrSgUevdHdorUDfYjvB3Kb-pjpZEyU18wTUTMg1Pb8Zjwluf_nuYM32K7N7TZlc-mdIwy8VX956eO14hZJNLFAiaf4xQskOCPiuahxD22kn1wpGEbShwE4TJYAsXTlT1xhA86CmDZwxaXSGzuYaNcpsokFCwAPkJ6axQbkPowO_CYrY9Ma5s0fwqdnCyZeUCl5ugeK-T9DtzuXBPsXi5eVky2rvqcO6cXCGCdrNfKXnc7Ehwtrv_ZxaUg0ORk_qVuGPWtfj0o9Ww2hUe8mXh7gp3d74jkDwNHV6X4S4qLApICRDU-Fw3kHZP5S1nvunjE_0FT4f3b4-SR_HAhjwl4-Rw4pRbX4Vu--CK6NfSgmNsixyapPm28tAzDfkHTDhRcHUi67fwGTEYKQvdk0JlV6zPSPopot2sRWRakyy_31oPCtz0qfavP0YT2dJWYGR9cSaJYIxz6S2cK2C0UOIIZw-TGX5zOnOzha1F-0Q3xPOswpHczoe-TafW3F22F5juRoNk7mK-BinUc4RBfBUkQWi6xTF32mLonuoeUkDRamMPX35P8ejYzBB2eDBb16QHIKyGl8R0aV_UYU5C661dG9xKvGJ98VHzFcz5j1J9WutBaMAeTRtROqRHH00psYa0oP2M4qVWR9eEP-jK9jtL8CdFGpNOiCtB7yF9WfE9N20Pqry88TODVMiT-MebZRHhvV3WCv3A3ZMgtVRqwaPoFp33I5GEDYuqDcHED_b4qZf65ZLNp5ptBSA_faSoeUJiRKRm8fep712gWgDS_fDmhdwfgIquY1fHZkofED0k_em28t0EAdJPpv0QqIKcte2vHkl65ql720YXWmYchBpDnclEMpExJg8T2CSd90jHvypi7KaPX2ahKv9zB1qlBTOFST4SWH_2I2_Tfvyhdqud7-ZWO_KFX9blDlph7WVs9rfWXjWV6zPEVquYDL-dJw5Lbk6mtHp2W7Z0ULHPrJqjLfqwjo4gcmD1TLm6LMVVYi7B2jb-_Vum9sxrWkIvVILP-LoCI1MYRvCj1QjrmO9ZFfqAGoIud_BYDKbnU3NoBJ0eWsvucGhrzaZ4-BYj_cOdUblQu08kmCHuxdhzSh8rZ4-vFrQ-9ZP83lHDA0tX5_F2MT_LoZQhph_NedK1bhhzBPaB4och2sY83So8dmgfcIlJXOkV_K6jO517pkHR7FBO-vQcD4850F7StoFZQoGRC4nqPTX6jCjMCNOMKoN1CQ58rmXrAI5LVtfo78iErXCXpsc-IO9HPfRSK-6PwA7cETFyy_To8ZjgozhJg6XUjxnYv4wD-9SpKMJwH39eihFj_bXWhXoHeZmXB8_8YSPf5L0ADirzoo6Pkxr6-zy1AmfNFrGXI-Zfb5BC6mRfRM3g9cTLr_fN3tESHnm3wzSh5dO3gpSHhE21eADFj7A6be8p2BgvvQl_ecm6KwPyO56-0HC4jxLTgZhyhtDuULWS4J6EPN6p2kE_EB_x7N5-RibkqfsbGMHMIRjKR7uzliG-WeYemvBdtkX7AmGuNVpRd_30fHmnE_yZoGdcrJjSeycv5EdqT7CeTBziIK2R78REHroEUJ23C3lvbfrH6Tz8XJ2ktokWxPXRqbwoqd3tcN7T9PoUgSvuSLJ32a7oMGzuYYOTS6JUWmBvGihfqKng-Pf9dxyZCSZ24nhxHNbw3HqW4gKI5viL5d9jUPtaT_GQKOEHNJpDeVMdV7lGgCbogkg5l4te0onSXDOCGEdnSvS3iuqtvT4-zo9tyrWIjUl5NQ8sZiK82-Nd_szEXjJzG88Xq6-oTZEUBqryI0FZjYNkE-ME-6L-zhR8hQdxwAwNMFXHW3EVT0ScCVYCo4EAGeQnQ-HnBEw7MouFlRLiNAf-50hOUBkBau8O8yHey1nrj2bsaeUcAksNUnELXiyCxhKemwFiZvLhCVgh5TTbVYw_Y89DA-MM1zwXC-k6IMqSW7JeCZogoeWr3Zx7l4eps0OvLUoqrAFV3ll8I_XToK-Ik6VQ68VdPj4GazF6cWaXnAHLfvJG_aXcGrAYpZtgUgClHOh2OwHZmCZMSYHdQCxQimnLelRfETan7cqYrkEaWJYUgP3UC3r4sP-JqEaWyYAoFvUoQ9G9U4msM82pagm3qmlXSSbKLPSD3tDwXXPJvZWfghw1VU8ZFpmiLKXanJ5eifWcwoDPPIuAAxYfKhsppg6S98JvBXHLkAchNovaxpjZopRMOkB1kjGBL_Y09d3D7MosAZKfZ6rloz6pHtrHz2RK7OaLQnrKZGVvYm3kuPRexi6p5gQ0wdPNMUNUU02FXBE1s_EASMX595qZE-zQ70Sor5l7YQ1CtM0EUaks1sVwRLyt8dyRi-ezsr6zNZFE4DTP7LUF96Mhb7wesntpOEOLTHeLbAJcC4rXUIN1guzU4vxEa8zqUzjEKy9zqQAUZ2e1mLpGDSoiYbdYHAmX1KRpfaTiUg6uXolUA60Vo-LAzlZ0ddU_NXOJJNSkx-F-U_0vcblf6qxTSUp35ppfxtP0_859WOIgWULId4jSEvi0hHUh2YGgbwh_uIGygjzISEHEdDHJ5mKDFrsGB_Wsw8VSM9-dPvf4C_ShvwxUkvuZh5_A9sjjUoVbz5nLcgTTsMMgM7xbwq-4E7dWKKMFy0gcy856lu5BlAIQ0VGtt6Cx3LtJ9aqOGchSiplhka67509gHuw33iPew2vdleAtJ3TAcSXkBg4Mw2dFqfWRH06gsm5pYjN8JR6RZHrxv8ldssKvV8E6udVUCRE-SkCMxbvCbSbZxNwD5AkbzbKlqf9pCtYNe9ThlgmV9HR7UE6pWPefCloH2DGs-iRSmWLUX1nyqQgs5KOX4YkWAi6He3IMMoFTL1SPNg1Z77RJZAYKvU2Hu2OK5mzUvyFcTLA3u8U6G6DUFjM3NFdhlEaxEbi6pAt7ChK2rRtGGUgJEEhIXU3_yqj5SGGJNplh576m4_Kg0o8XGjmz8LmbOhZflgMrVZsQUJ4UulqWpBOt5PBeBYhRNTi7EeBWQ7bCWQHYHdxJlsGb113wliz9vmtEov61LHubQV1-AAm-gp_FDOCZc9P4nv-auWzbxxjpdHMfngYPkEx6mrga6kst_7RsTkcedKFYjppmIKRtAi3MT2cBcsBGGblqdJ4tT6BOyDRIo3g8mw8RrbQeSKE3w27-tX3F9vJ9c68OMJ4vRsDHXg9XzIozhN7S0Jure-IUoOcG9XpgdW9xHuH_kcb-gg8v4MMRZn0EzPZRWIdVTpxIy7DWvHGgqYIG0JzKCXU3zaCnLow4XtahxVXWH6FNTi9z5dhiMPYIasbCVHMGmg6mjk5WwH0L2zqGsRBr_26ghrd6Pd_yxKk68IrTK7NCsHIvQHGJoqnepklu86AZbb0bST344sXJu5ulkZXMedg9JwrDSNAdmt0I57JmqEG6-29YUnC1V1IysfdttHIc59SNNHRrM4PtLh1_1qwJRNoFvJ8SxQv_GEd3GKf0tUv6kanuOGmof9thHRAozrTDhlms3_qrVe-6OWWAEiapvIRbZCp-unGW8xLHD3bH2pz47dZSTIme3LVAyNALhVV-u195ZUXz3yDKXsR1_uHBbvhk8-4PVKC1QBuZ_VF6frsYSIqoNkbOkOrNGieIWH56MnBjgaUtTYpAFo4U8zaX6TXm-xszNDotUbk269v-9mRh_mC4iBWI3Kwbbl8rbD_hVv_K8A87EJ2Z4ev53TNRrFsMyxlQjWfYdsR0Hfnvi8dFXqEgSU5M3m2RjlKccSaTcu22dt9LVaTBgTdzxrmyJhdtCJ76fpV4NoWiS8YMsK4l1d_e_Ceu4QKnVeJ-dfCUHLvGNnOZlfLu8Qc_ebPL81x1jzxOTo_Xb7GxQeJ35FUM1Zit4_yametU9kPdOUkRdudNlT9GIfwrk6FpM1f9_fNqLzklnEn-YjS5xij7zPS-bRVeRRQidzDEqm1ORi5S2nCoGlNNn_LAjbnSCbWynvfK2lvMQTZuh1RvC7eoLrGhQIStpC8DgjUa_DhXgkksZes1YeqE9vCSsOcPRsiChgyqmkOq36h_gzkDGCLDWyBRHM-M2upAD2DUNw0wFxCbVnA7bFTRL2CZ9K5uYzRFFMdI2txIKZXJ9Dq4LRcYFcLKWnkfHHqmZUT2cSvXToh2VzUNvGR-PZvcGuVRTZRPTF0YasdtEtPmAVTT8UMhkUfd2pl2SCpo336MXystN-bf9NqEC35OJXW3WGCQ1Z9KcOOvArnm0Ukrf1WcaM0w4zD9xpaKIXGYugaW-9ngUhq0eKfJe7wTuApEdGNIn3hRTGkjmxicYdcg.qmYQAxREDxNoAyCwk0e1Dw";
            payload.Add(Constants.PAYLOAD, outputPayload);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            BaseRequest baseRequest = new BaseRequest(payload);
            Assert.AreEqual(HCXIntegrator.config.ParticipantCode, baseRequest.GetHcxSenderCode());

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj with 3 Object  with (timestamp, api_call_id, correlation_id)
        }

        [TestMethod] //(6)
        public void HcxIncomingOnrequestSuccess()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string outputPayload = "eyJ4LWhjeC1yZWNpcGllbnRfY29kZSI6InRlc3Rwcm92aWRlcjEuYXBvbGxvQHN3YXN0aC1oY3gtZGV2IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0xMC0yN1QxMTowNzo0OCswNTMwIiwieC1oY3gtc2VuZGVyX2NvZGUiOiJ0ZXN0cHJvdmlkZXIxLmFwb2xsb0Bzd2FzdGgtaGN4LWRldiIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiZDRmNTZkNzktNDkwOC00YTk5LWE4ZGQtYTNiNzMzZmRlOGQ2IiwiZW5jIjoiQTI1NkdDTSIsImFsZyI6IlJTQS1PQUVQLTI1NiIsIngtaGN4LWFwaV9jYWxsX2lkIjoiMWUxNzk3YmQtZGJlZC00MTkyLWIwYzktY2VmNzcyNzI0YmU1In0.NSYks0P3BizbgpGF7GctpBSFDfSfap2V7AnZ5YCQMy_V0F6IZ1weRbZrBLHDTnPwPOBGGfctXpyqiXfvldMjCI_maakNjagsyC2x0pFC6NGmYhTwjqWmpL2CCaneBf9HikqwuI2cJTK8-DNOkbT9Qj8j-NxyGv1NX8UFI1K90t9e61qJ_Xurp6Qrrt6X_fiW7Jx9Vm54kCS7ZUfGK2rw_EOvc1VydsdWnUABOcmbtcTJiSVecNdRYxiKAIsiZCHULdd92a3hzbqFbyfRu1GqmuEyoimjd7jvdDSuB4bhE6WCIF-wB9Z5d3mj1ZXS8AEGT9YuSFLQTyPfo9Di3-Px-Q.0MliuZkDRADgQXY5.LWL2_vhMA8BEYtmg8SFXVoDNWvwWLZcJ9KxOHR7VWdIRxsO7PfNk7yMhctRtmAnyaaNZYZb0e3nVTai-u9aSixhmbxq6rFe3an0HbPi43BIT4ytal-CRxWoTaoBMZLmNqSr7GAvAXxNxtkSpDRUMlGg-tGdrHmcW6b1KAbA2CxE62XlaMMt5DNarXMSBDMx1_Pz0x9QqwFY-8O1GOIwPVZF59fh2e8ytlsvTh1fgiMZSOLETzN5pgCWGacc1D-tSmDD5kDQZm3SPgwZMbkco2xmKdvfdZa1AX7rDrYO6Pl-WFxv_5SO1CWwtR45tsCCp4pIOv3VUIjwv2YlmoTgXXg-EoLNgA4N8sw4kXLNtV9hMys2JdyQ93quEcFGnPO9jMg9seAT3pq_llpkN1FqE2x7zwWFeAs0g48IFc-39td6kdgOnuVEVsGAyE_OEst8TAhvRBa7EOeZmZpM1fiANdLor2xKWOI2K3o52R2XOYnchOWwjF07eWOPYulvSZQPBc1LZ_1j1myJz4qNH6aKWxkFT99FBddQ_UA49SgHRhYNScs27Ycw0BEZso9UpiXoLDJUKIp0Q717BkM5K1DdwYixkIMSEAM2HZHRaD0fk_8C0Xx0C1WxpZAhOtjKkesd1O2iUA2YbOyClgdASSUjO9KWrSgUevdHdorUDfYjvB3Kb-pjpZEyU18wTUTMg1Pb8Zjwluf_nuYM32K7N7TZlc-mdIwy8VX956eO14hZJNLFAiaf4xQskOCPiuahxD22kn1wpGEbShwE4TJYAsXTlT1xhA86CmDZwxaXSGzuYaNcpsokFCwAPkJ6axQbkPowO_CYrY9Ma5s0fwqdnCyZeUCl5ugeK-T9DtzuXBPsXi5eVky2rvqcO6cXCGCdrNfKXnc7Ehwtrv_ZxaUg0ORk_qVuGPWtfj0o9Ww2hUe8mXh7gp3d74jkDwNHV6X4S4qLApICRDU-Fw3kHZP5S1nvunjE_0FT4f3b4-SR_HAhjwl4-Rw4pRbX4Vu--CK6NfSgmNsixyapPm28tAzDfkHTDhRcHUi67fwGTEYKQvdk0JlV6zPSPopot2sRWRakyy_31oPCtz0qfavP0YT2dJWYGR9cSaJYIxz6S2cK2C0UOIIZw-TGX5zOnOzha1F-0Q3xPOswpHczoe-TafW3F22F5juRoNk7mK-BinUc4RBfBUkQWi6xTF32mLonuoeUkDRamMPX35P8ejYzBB2eDBb16QHIKyGl8R0aV_UYU5C661dG9xKvGJ98VHzFcz5j1J9WutBaMAeTRtROqRHH00psYa0oP2M4qVWR9eEP-jK9jtL8CdFGpNOiCtB7yF9WfE9N20Pqry88TODVMiT-MebZRHhvV3WCv3A3ZMgtVRqwaPoFp33I5GEDYuqDcHED_b4qZf65ZLNp5ptBSA_faSoeUJiRKRm8fep712gWgDS_fDmhdwfgIquY1fHZkofED0k_em28t0EAdJPpv0QqIKcte2vHkl65ql720YXWmYchBpDnclEMpExJg8T2CSd90jHvypi7KaPX2ahKv9zB1qlBTOFST4SWH_2I2_Tfvyhdqud7-ZWO_KFX9blDlph7WVs9rfWXjWV6zPEVquYDL-dJw5Lbk6mtHp2W7Z0ULHPrJqjLfqwjo4gcmD1TLm6LMVVYi7B2jb-_Vum9sxrWkIvVILP-LoCI1MYRvCj1QjrmO9ZFfqAGoIud_BYDKbnU3NoBJ0eWsvucGhrzaZ4-BYj_cOdUblQu08kmCHuxdhzSh8rZ4-vFrQ-9ZP83lHDA0tX5_F2MT_LoZQhph_NedK1bhhzBPaB4och2sY83So8dmgfcIlJXOkV_K6jO517pkHR7FBO-vQcD4850F7StoFZQoGRC4nqPTX6jCjMCNOMKoN1CQ58rmXrAI5LVtfo78iErXCXpsc-IO9HPfRSK-6PwA7cETFyy_To8ZjgozhJg6XUjxnYv4wD-9SpKMJwH39eihFj_bXWhXoHeZmXB8_8YSPf5L0ADirzoo6Pkxr6-zy1AmfNFrGXI-Zfb5BC6mRfRM3g9cTLr_fN3tESHnm3wzSh5dO3gpSHhE21eADFj7A6be8p2BgvvQl_ecm6KwPyO56-0HC4jxLTgZhyhtDuULWS4J6EPN6p2kE_EB_x7N5-RibkqfsbGMHMIRjKR7uzliG-WeYemvBdtkX7AmGuNVpRd_30fHmnE_yZoGdcrJjSeycv5EdqT7CeTBziIK2R78REHroEUJ23C3lvbfrH6Tz8XJ2ktokWxPXRqbwoqd3tcN7T9PoUgSvuSLJ32a7oMGzuYYOTS6JUWmBvGihfqKng-Pf9dxyZCSZ24nhxHNbw3HqW4gKI5viL5d9jUPtaT_GQKOEHNJpDeVMdV7lGgCbogkg5l4te0onSXDOCGEdnSvS3iuqtvT4-zo9tyrWIjUl5NQ8sZiK82-Nd_szEXjJzG88Xq6-oTZEUBqryI0FZjYNkE-ME-6L-zhR8hQdxwAwNMFXHW3EVT0ScCVYCo4EAGeQnQ-HnBEw7MouFlRLiNAf-50hOUBkBau8O8yHey1nrj2bsaeUcAksNUnELXiyCxhKemwFiZvLhCVgh5TTbVYw_Y89DA-MM1zwXC-k6IMqSW7JeCZogoeWr3Zx7l4eps0OvLUoqrAFV3ll8I_XToK-Ik6VQ68VdPj4GazF6cWaXnAHLfvJG_aXcGrAYpZtgUgClHOh2OwHZmCZMSYHdQCxQimnLelRfETan7cqYrkEaWJYUgP3UC3r4sP-JqEaWyYAoFvUoQ9G9U4msM82pagm3qmlXSSbKLPSD3tDwXXPJvZWfghw1VU8ZFpmiLKXanJ5eifWcwoDPPIuAAxYfKhsppg6S98JvBXHLkAchNovaxpjZopRMOkB1kjGBL_Y09d3D7MosAZKfZ6rloz6pHtrHz2RK7OaLQnrKZGVvYm3kuPRexi6p5gQ0wdPNMUNUU02FXBE1s_EASMX595qZE-zQ70Sor5l7YQ1CtM0EUaks1sVwRLyt8dyRi-ezsr6zNZFE4DTP7LUF96Mhb7wesntpOEOLTHeLbAJcC4rXUIN1guzU4vxEa8zqUzjEKy9zqQAUZ2e1mLpGDSoiYbdYHAmX1KRpfaTiUg6uXolUA60Vo-LAzlZ0ddU_NXOJJNSkx-F-U_0vcblf6qxTSUp35ppfxtP0_859WOIgWULId4jSEvi0hHUh2YGgbwh_uIGygjzISEHEdDHJ5mKDFrsGB_Wsw8VSM9-dPvf4C_ShvwxUkvuZh5_A9sjjUoVbz5nLcgTTsMMgM7xbwq-4E7dWKKMFy0gcy856lu5BlAIQ0VGtt6Cx3LtJ9aqOGchSiplhka67509gHuw33iPew2vdleAtJ3TAcSXkBg4Mw2dFqfWRH06gsm5pYjN8JR6RZHrxv8ldssKvV8E6udVUCRE-SkCMxbvCbSbZxNwD5AkbzbKlqf9pCtYNe9ThlgmV9HR7UE6pWPefCloH2DGs-iRSmWLUX1nyqQgs5KOX4YkWAi6He3IMMoFTL1SPNg1Z77RJZAYKvU2Hu2OK5mzUvyFcTLA3u8U6G6DUFjM3NFdhlEaxEbi6pAt7ChK2rRtGGUgJEEhIXU3_yqj5SGGJNplh576m4_Kg0o8XGjmz8LmbOhZflgMrVZsQUJ4UulqWpBOt5PBeBYhRNTi7EeBWQ7bCWQHYHdxJlsGb113wliz9vmtEov61LHubQV1-AAm-gp_FDOCZc9P4nv-auWzbxxjpdHMfngYPkEx6mrga6kst_7RsTkcedKFYjppmIKRtAi3MT2cBcsBGGblqdJ4tT6BOyDRIo3g8mw8RrbQeSKE3w27-tX3F9vJ9c68OMJ4vRsDHXg9XzIozhN7S0Jure-IUoOcG9XpgdW9xHuH_kcb-gg8v4MMRZn0EzPZRWIdVTpxIy7DWvHGgqYIG0JzKCXU3zaCnLow4XtahxVXWH6FNTi9z5dhiMPYIasbCVHMGmg6mjk5WwH0L2zqGsRBr_26ghrd6Pd_yxKk68IrTK7NCsHIvQHGJoqnepklu86AZbb0bST344sXJu5ulkZXMedg9JwrDSNAdmt0I57JmqEG6-29YUnC1V1IysfdttHIc59SNNHRrM4PtLh1_1qwJRNoFvJ8SxQv_GEd3GKf0tUv6kanuOGmof9thHRAozrTDhlms3_qrVe-6OWWAEiapvIRbZCp-unGW8xLHD3bH2pz47dZSTIme3LVAyNALhVV-u195ZUXz3yDKXsR1_uHBbvhk8-4PVKC1QBuZ_VF6frsYSIqoNkbOkOrNGieIWH56MnBjgaUtTYpAFo4U8zaX6TXm-xszNDotUbk269v-9mRh_mC4iBWI3Kwbbl8rbD_hVv_K8A87EJ2Z4ev53TNRrFsMyxlQjWfYdsR0Hfnvi8dFXqEgSU5M3m2RjlKccSaTcu22dt9LVaTBgTdzxrmyJhdtCJ76fpV4NoWiS8YMsK4l1d_e_Ceu4QKnVeJ-dfCUHLvGNnOZlfLu8Qc_ebPL81x1jzxOTo_Xb7GxQeJ35FUM1Zit4_yametU9kPdOUkRdudNlT9GIfwrk6FpM1f9_fNqLzklnEn-YjS5xij7zPS-bRVeRRQidzDEqm1ORi5S2nCoGlNNn_LAjbnSCbWynvfK2lvMQTZuh1RvC7eoLrGhQIStpC8DgjUa_DhXgkksZes1YeqE9vCSsOcPRsiChgyqmkOq36h_gzkDGCLDWyBRHM-M2upAD2DUNw0wFxCbVnA7bFTRL2CZ9K5uYzRFFMdI2txIKZXJ9Dq4LRcYFcLKWnkfHHqmZUT2cSvXToh2VzUNvGR-PZvcGuVRTZRPTF0YasdtEtPmAVTT8UMhkUfd2pl2SCpo336MXystN-bf9NqEC35OJXW3WGCQ1Z9KcOOvArnm0Ukrf1WcaM0w4zD9xpaKIXGYugaW-9ngUhq0eKfJe7wTuApEdGNIn3hRTGkjmxicYdcg.qmYQAxREDxNoAyCwk0e1Dw";
            payload.Add(Constants.PAYLOAD, outputPayload);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_ON_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > Mandatory headers are missing: x-hcx-status
        }

        [TestMethod] //(7)
        public void HcxIncomingErrorDetails()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string errorDetails = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1lcnJvcl9kZXRhaWxzIjp7CiJjb2RlIjoiRVJSX0lOVkFMSURfUEFZTE9BRCIsCiJtZXNzYWdlIjoiaW52YWxpZCByZXF1ZXN0IHBheWxvYWQiLAoidHJhY2UiOiIiLAoidGVzdCI6IiJ9fQ==.6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY.Mz-VPPyU4RlcuYv1IwIvzw";
            payload.Add(Constants.PAYLOAD, errorDetails);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_ENCRYPTION - Decryption error

            //Note : ERROR_DETAILS Header error (not shown due to Decryption error but INTERNAL)  [Same as JAVA SDK]
            //Error details should contain only: code, message, trace
            //Status value for action API calls can be only: request.queued, request.dispatched
        }

        [TestMethod] //(8)
        public void HcxIncomingDebugDetails()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string debugDetails = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1kZWJ1Z19kZXRhaWxzIjp7CiJjb2RlIjoiRVJSX0lOVkFMSURfUEFZTE9BRCIsCiJtZXNzYWdlIjoiaW52YWxpZCByZXF1ZXN0IHBheWxvYWQiLAoidHJhY2UiOiIiLAoidGVzdCI6IiJ9fQ==.6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY.Mz-VPPyU4RlcuYv1IwIvzw";
            payload.Add(Constants.PAYLOAD, debugDetails);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_ENCRYPTION - Decryption error

            //Note : DEBUG_DETAILS Header error (not shown due to Decryption error but INTERNAL)  [Same as JAVA SDK]
            //Debug details should contain only: code, message, trace
            //Status value for action API calls can be only: request.queued, request.dispatched
        }

        [TestMethod] //(9)
        public void HcxIncomingDebugFlag()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string debugFlag = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1kZWJ1Z19mbGFnIjoidGVzdCIKfQ==.6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY.Mz-VPPyU4RlcuYv1IwIvzw";
            payload.Add(Constants.PAYLOAD, debugFlag);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_ENCRYPTION - Decryption error

            //Note : DEBUG_FLAG Header error (not shown due to Decryption error but INTERNAL)  [Same as JAVA SDK]
            //Debug flag cannot be other than Error, Info, Debug
            //Status value for action API calls can be only: request.queued, request.dispatched
        }

        [TestMethod] //(10)
        public void HcxIncomingErrorDetailsNull()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string errorDetailsEmpty = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1lcnJvcl9kZXRhaWxzIjpudWxsfQ==.6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY.Mz-VPPyU4RlcuYv1IwIvzw";
            payload.Add(Constants.PAYLOAD, errorDetailsEmpty);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_ENCRYPTION - Decryption error

            //Note : ERROR_DETAILS Header error (not shown due to Decryption error but INTERNAL)  [Same as JAVA SDK]
            //Error details cannot be null, empty and other than 'JSON Object' with mandatory fields code or message
            //Status value for action API calls can be only: request.queued, request.dispatched
        }

        [TestMethod] //(11)
        public void HcxIncomingErrorDetailsCodeMessageEmpty()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string errorDetailsCodeMessage = "eyJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAiLCJ4LWhjeC1zZW5kZXJfY29kZSI6IjEtM2EzYmQ2OGEtODQ4YS00ZDUyLTllYzItMDdhOTJkNzY1ZmI0IiwieC1oY3gtcmVjaXBpZW50X2NvZGUiOiIxLTdiYTA3ZTMxLWNlYmItNDc1MS1iMmI3LWZlMDUwZDlkMmMwMCIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiODU0ZmU0MWItMjEyZi00YTU1LWJlMmYtMTBiZGE4ZGFkYzk1IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0wNS0xMlQxNToyNjoxOS42MjcrMDUzMCIsIngtaGN4LWFwaV9jYWxsX2lkIjoiYWExZTNmOWItOTBhNy00ZWQ5LTk4MjEtMDMwNmYxYmNiNzQ2IiwieC1oY3gtd29ya2Zsb3dfaWQiOiI1ZTkzNGY5MC0xMTFkLTRmMGItYjAxNi1jMjJkODIwNjc0ZTIiLCJ4LWhjeC1zdGF0dXMiOiJyZXNwb25zZS5jb21wbGV0ZSIsCiJ4LWhjeC1lcnJvcl9kZXRhaWxzIjp7CiJ0cmFjZSI6IiJ9fQ==.6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY.Mz-VPPyU4RlcuYv1IwIvzw";
            payload.Add(Constants.PAYLOAD, errorDetailsCodeMessage);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_ENCRYPTION - Decryption error

            //Note : ERROR_DETAILS Header error (not shown due to Decryption error but INTERNAL)  [Same as JAVA SDK]
            //Error details cannot be null, empty and other than 'JSON Object' with mandatory fields code or message
            //Status value for action API calls can be only: request.queued, request.dispatched
        }

        [TestMethod] //(12)
        public void HcxOutgoingRequestRecipientCodeNull()
        {
            InitializeConfig();
            HCXOutgoingRequest hcxOutgoingRequest = new HCXOutgoingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            hcxOutgoingRequest.Generate(commonFhirPayload, Operations.COVERAGE_ELIGIBILITY_CHECK, null, output);
            BaseRequest baseRequest = new BaseRequest(output);
            Assert.IsFalse(commonFhirPayload.IsNullOrEmpty());

            //Note Output :   [Same in JAVA SDK]
            //Output : Error while creating headers: Object reference not set to an instance of an object.
        }

        [TestMethod] //(13)
        public void HcxIncomingRequestInvalidPayload()
        {
            InitializeConfig();
            HCXIncomingRequest hcxIncomingRequest = new HCXIncomingRequest();
            Dictionary<string, object> output = new Dictionary<string, object>();
            Dictionary<string, object> payload = new Dictionary<string, object>();
            string outputPayload = "eyJ4LWhjeC1yZWNpcGllbnRfY29kZSI6InRlc3Rwcm92aWRlcjEuYXBvbGxvQHN3YXN0aC1oY3gtZGV2IiwieC1oY3gtdGltZXN0YW1wIjoiMjAyMi0xMC0yN1QxMTowNzo0OCswNTMwIiwieC1oY3gtc2VuZGVyX2NvZGUiOiJ0ZXN0cHJvdmlkZXIxLmFwb2xsb0Bzd2FzdGgtaGN4LWRldiIsIngtaGN4LWNvcnJlbGF0aW9uX2lkIjoiZDRmNTZkNzktNDkwOC00YTk5LWE4ZGQtYTNiNzMzZmRlOGQ2IiwiZW5jIjoiQTI1NkdDTSIsImFsZyI6IlJTQS1PQUVQLTI1NiIsIngtaGN4LWFwaV9jYWxsX2lkIjoiMWUxNzk3YmQtZGJlZC00MTkyLWIwYzktY2VmNzcyNzI0YmU1In0.NSYks0P3BizbgpGF7GctpBSFDfSfap2V7AnZ5YCQMy_V0F6IZ1weRbZrBLHDTnPwPOBGGfctXpyqiXfvldMjCI_maakNjagsyC2x0pFC6NGmYhTwjqWmpL2CCaneBf9HikqwuI2cJTK8-DNOkbT9Qj8j-NxyGv1NX8UFI1K90t9e61qJ_Xurp6Qrrt6X_fiW7Jx9Vm54kCS7ZUfGK2rw_EOvc1VydsdWnUABOcmbtcTJiSVecNdRYxiKAIsiZCHULdd92a3hzbqFbyfRu1GqmuEyoimjd7jvdDSuB4bhE6WCIF-wB9Z5d3mj1ZXS8AEGT9YuSFLQTyPfo9Di3-Px-Q.0MliuZkDRADgQXY5.LWL2_vhMA8BEYtmg8SFXVoDNWvwWLZcJ9KxOHR7VWdIRxsO7PfNk7yMhctRtmAnyaaNZYZb0e3nVTai-u9aSixhmbxq6rFe3an0HbPi43BIT4ytal-CRxWoTaoBMZLmNqSr7GAvAXxNxtkSpDRUMlGg-tGdrHmcW6b1KAbA2CxE62XlaMMt5DNarXMSBDMx1_Pz0x9QqwFY-8O1GOIwPVZF59fh2e8ytlsvTh1fgiMZSOLETzN5pgCWGacc1D-tSmDD5kDQZm3SPgwZMbkco2xmKdvfdZa1AX7rDrYO6Pl-WFxv_5SO1CWwtR45tsCCp4pIOv3VUIjwv2YlmoTgXXg-EoLNgA4N8sw4kXLNtV9hMys2JdyQ93quEcFGnPO9jMg9seAT3pq_llpkN1FqE2x7zwWFeAs0g48IFc-39td6kdgOnuVEVsGAyE_OEst8TAhvRBa7EOeZmZpM1fiANdLor2xKWOI2K3o52R2XOYnchOWwjF07eWOPYulvSZQPBc1LZ_1j1myJz4qNH6aKWxkFT99FBddQ_UA49SgHRhYNScs27Ycw0BEZso9UpiXoLDJUKIp0Q717BkM5K1DdwYixkIMSEAM2HZHRaD0fk_8C0Xx0C1WxpZAhOtjKkesd1O2iUA2YbOyClgdASSUjO9KWrSgUevdHdorUDfYjvB3Kb-pjpZEyU18wTUTMg1Pb8Zjwluf_nuYM32K7N7TZlc-mdIwy8VX956eO14hZJNLFAiaf4xQskOCPiuahxD22kn1wpGEbShwE4TJYAsXTlT1xhA86CmDZwxaXSGzuYaNcpsokFCwAPkJ6axQbkPowO_CYrY9Ma5s0fwqdnCyZeUCl5ugeK-T9DtzuXBPsXi5eVky2rvqcO6cXCGCdrNfKXnc7Ehwtrv_ZxaUg0ORk_qVuGPWtfj0o9Ww2hUe8mXh7gp3d74jkDwNHV6X4S4qLApICRDU-Fw3kHZP5S1nvunjE_0FT4f3b4-SR_HAhjwl4-Rw4pRbX4Vu--CK6NfSgmNsixyapPm28tAzDfkHTDhRcHUi67fwGTEYKQvdk0JlV6zPSPopot2sRWRakyy_31oPCtz0qfavP0YT2dJWYGR9cSaJYIxz6S2cK2C0UOIIZw-TGX5zOnOzha1F-0Q3xPOswpHczoe-TafW3F22F5juRoNk7mK-BinUc4RBfBUkQWi6xTF32mLonuoeUkDRamMPX35P8ejYzBB2eDBb16QHIKyGl8R0aV_UYU5C661dG9xKvGJ98VHzFcz5j1J9WutBaMAeTRtROqRHH00psYa0oP2M4qVWR9eEP-jK9jtL8CdFGpNOiCtB7yF9WfE9N20Pqry88TODVMiT-MebZRHhvV3WCv3A3ZMgtVRqwaPoFp33I5GEDYuqDcHED_b4qZf65ZLNp5ptBSA_faSoeUJiRKRm8fep712gWgDS_fDmhdwfgIquY1fHZkofED0k_em28t0EAdJPpv0QqIKcte2vHkl65ql720YXWmYchBpDnclEMpExJg8T2CSd90jHvypi7KaPX2ahKv9zB1qlBTOFST4SWH_2I2_Tfvyhdqud7-ZWO_KFX9blDlph7WVs9rfWXjWV6zPEVquYDL-dJw5Lbk6mtHp2W7Z0ULHPrJqjLfqwjo4gcmD1TLm6LMVVYi7B2jb-_Vum9sxrWkIvVILP-LoCI1MYRvCj1QjrmO9ZFfqAGoIud_BYDKbnU3NoBJ0eWsvucGhrzaZ4-BYj_cOdUblQu08kmCHuxdhzSh8rZ4-vFrQ-9ZP83lHDA0tX5_F2MT_LoZQhph_NedK1bhhzBPaB4och2sY83So8dmgfcIlJXOkV_K6jO517pkHR7FBO-vQcD4850F7StoFZQoGRC4nqPTX6jCjMCNOMKoN1CQ58rmXrAI5LVtfo78iErXCXpsc-IO9HPfRSK-6PwA7cETFyy_To8ZjgozhJg6XUjxnYv4wD-9SpKMJwH39eihFj_bXWhXoHeZmXB8_8YSPf5L0ADirzoo6Pkxr6-zy1AmfNFrGXI-Zfb5BC6mRfRM3g9cTLr_fN3tESHnm3wzSh5dO3gpSHhE21eADFj7A6be8p2BgvvQl_ecm6KwPyO56-0HC4jxLTgZhyhtDuULWS4J6EPN6p2kE_EB_x7N5-RibkqfsbGMHMIRjKR7uzliG-WeYemvBdtkX7AmGuNVpRd_30fHmnE_yZoGdcrJjSeycv5EdqT7CeTBziIK2R78REHroEUJ23C3lvbfrH6Tz8XJ2ktokWxPXRqbwoqd3tcN7T9PoUgSvuSLJ32a7oMGzuYYOTS6JUWmBvGihfqKng-Pf9dxyZCSZ24nhxHNbw3HqW4gKI5viL5d9jUPtaT_GQKOEHNJpDeVMdV7lGgCbogkg5l4te0onSXDOCGEdnSvS3iuqtvT4-zo9tyrWIjUl5NQ8sZiK82-Nd_szEXjJzG88Xq6-oTZEUBqryI0FZjYNkE-ME-6L-zhR8hQdxwAwNMFXHW3EVT0ScCVYCo4EAGeQnQ-HnBEw7MouFlRLiNAf-50hOUBkBau8O8yHey1nrj2bsaeUcAksNUnELXiyCxhKemwFiZvLhCVgh5TTbVYw_Y89DA-MM1zwXC-k6IMqSW7JeCZogoeWr3Zx7l4eps0OvLUoqrAFV3ll8I_XToK-Ik6VQ68VdPj4GazF6cWaXnAHLfvJG_aXcGrAYpZtgUgClHOh2OwHZmCZMSYHdQCxQimnLelRfETan7cqYrkEaWJYUgP3UC3r4sP-JqEaWyYAoFvUoQ9G9U4msM82pagm3qmlXSSbKLPSD3tDwXXPJvZWfghw1VU8ZFpmiLKXanJ5eifWcwoDPPIuAAxYfKhsppg6S98JvBXHLkAchNovaxpjZopRMOkB1kjGBL_Y09d3D7MosAZKfZ6rloz6pHtrHz2RK7OaLQnrKZGVvYm3kuPRexi6p5gQ0wdPNMUNUU02FXBE1s_EASMX595qZE-zQ70Sor5l7YQ1CtM0EUaks1sVwRLyt8dyRi-ezsr6zNZFE4DTP7LUF96Mhb7wesntpOEOLTHeLbAJcC4rXUIN1guzU4vxEa8zqUzjEKy9zqQAUZ2e1mLpGDSoiYbdYHAmX1KRpfaTiUg6uXolUA60Vo-LAzlZ0ddU_NXOJJNSkx-F-U_0vcblf6qxTSUp35ppfxtP0_859WOIgWULId4jSEvi0hHUh2YGgbwh_uIGygjzISEHEdDHJ5mKDFrsGB_Wsw8VSM9-dPvf4C_ShvwxUkvuZh5_A9sjjUoVbz5nLcgTTsMMgM7xbwq-4E7dWKKMFy0gcy856lu5BlAIQ0VGtt6Cx3LtJ9aqOGchSiplhka67509gHuw33iPew2vdleAtJ3TAcSXkBg4Mw2dFqfWRH06gsm5pYjN8JR6RZHrxv8ldssKvV8E6udVUCRE-SkCMxbvCbSbZxNwD5AkbzbKlqf9pCtYNe9ThlgmV9HR7UE6pWPefCloH2DGs-iRSmWLUX1nyqQgs5KOX4YkWAi6He3IMMoFTL1SPNg1Z77RJZAYKvU2Hu2OK5mzUvyFcTLA3u8U6G6DUFjM3NFdhlEaxEbi6pAt7ChK2rRtGGUgJEEhIXU3_yqj5SGGJNplh576m4_Kg0o8XGjmz8LmbOhZflgMrVZsQUJ4UulqWpBOt5PBeBYhRNTi7EeBWQ7bCWQHYHdxJlsGb113wliz9vmtEov61LHubQV1-AAm-gp_FDOCZc9P4nv-auWzbxxjpdHMfngYPkEx6mrga6kst_7RsTkcedKFYjppmIKRtAi3MT2cBcsBGGblqdJ4tT6BOyDRIo3g8mw8RrbQeSKE3w27-tX3F9vJ9c68OMJ4vRsDHXg9XzIozhN7S0Jure-IUoOcG9XpgdW9xHuH_kcb-gg8v4MMRZn0EzPZRWIdVTpxIy7DWvHGgqYIG0JzKCXU3zaCnLow4XtahxVXWH6FNTi9z5dhiMPYIasbCVHMGmg6mjk5WwH0L2zqGsRBr_26ghrd6Pd_yxKk68IrTK7NCsHIvQHGJoqnepklu86AZbb0bST344sXJu5ulkZXMedg9JwrDSNAdmt0I57JmqEG6-29YUnC1V1IysfdttHIc59SNNHRrM4PtLh1_1qwJRNoFvJ8SxQv_GEd3GKf0tUv6kanuOGmof9thHRAozrTDhlms3_qrVe-6OWWAEiapvIRbZCp-unGW8xLHD3bH2pz47dZSTIme3LVAyNALhVV-u195ZUXz3yDKXsR1_uHBbvhk8-4PVKC1QBuZ_VF6frsYSIqoNkbOkOrNGieIWH56MnBjgaUtTYpAFo4U8zaX6TXm-xszNDotUbk269v-9mRh_mC4iBWI3Kwbbl8rbD_hVv_K8A87EJ2Z4ev53TNRrFsMyxlQjWfYdsR0Hfnvi8dFXqEgSU5M3m2RjlKccSaTcu22dt9LVaTBgTdzxrmyJhdtCJ76fpV4NoWiS8YMsK4l1d_e_Ceu4QKnVeJ-dfCUHLvGNnOZlfLu8Qc_ebPL81x1jzxOTo_Xb7GxQeJ35FUM1Zit4_yametU9kPdOUkRdudNlT9GIfwrk6FpM1f9_fNqLzklnEn-YjS5xij7zPS-bRVeRRQidzDEqm1ORi5S2nCoGlNNn_LAjbnSCbWynvfK2lvMQTZuh1RvC7eoLrGhQIStpC8DgjUa_DhXgkksZes1YeqE9vCSsOcPRsiChgyqmkOq36h_gzkDGCLDWyBRHM-M2upAD2DUNw0wFxCbVnA7bFTRL2CZ9K5uYzRFFMdI2txIKZXJ9Dq4LRcYFcLKWnkfHHqmZUT2cSvXToh2VzUNvGR-PZvcGuVRTZRPTF0YasdtEtPmAVTT8UMhkUfd2pl2SCpo336MXystN-bf9NqEC35OJXW3WGCQ1Z9KcOOvArnm0Ukrf1WcaM0w4zD9xpaKIXGYugaW-9ngUhq0eKfJe7wTuApEdGNIn3hRTGkjmxicYdcg";
            payload.Add(Constants.PAYLOAD, outputPayload);
            hcxIncomingRequest.Process(JSONUtils.Serialize(payload), Operations.COVERAGE_ELIGIBILITY_CHECK, output);
            Assert.IsTrue(output.ContainsKey(Constants.RESPONSE_OBJ));

            //Note Output :   [Same in JAVA SDK]
            //Output : responseObj > ERR_INVALID_PAYLOAD : Mandatory elements of JWE token are missing.Should have all 5 elements
        }
    }
}
