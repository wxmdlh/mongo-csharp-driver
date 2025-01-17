﻿/* Copyright 2010-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Net.Http;
using MongoDB.Driver.Core.Misc;

namespace MongoDB.Driver.Core.Authentication.External
{
    internal class ExternalCredentialsAuthenticators
    {
        #region static
        private static Lazy<ExternalCredentialsAuthenticators> __instance = new Lazy<ExternalCredentialsAuthenticators>(() => new ExternalCredentialsAuthenticators(), isThreadSafe: true);
        // public static
        public static ExternalCredentialsAuthenticators Instance => __instance.Value;
        #endregion

        private readonly HttpClientHelper _httpClientHelper;
        private readonly Lazy<IExternalAuthenticationCredentialsProvider<AwsCredentials>> _awsExternalAuthenticationCredentialsProvider;
        private readonly Lazy<IExternalAuthenticationCredentialsProvider<GcpCredentials>> _gcpExternalAuthenticationCredentialsProvider;

        internal ExternalCredentialsAuthenticators() : this(new HttpClientHelper())
        {
        }

        internal ExternalCredentialsAuthenticators(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = Ensure.IsNotNull(httpClientHelper, nameof(httpClientHelper));
            _awsExternalAuthenticationCredentialsProvider = new Lazy<IExternalAuthenticationCredentialsProvider<AwsCredentials>>(() => new AwsAuthenticationCredentialsProvider(_httpClientHelper), isThreadSafe: true);
            _gcpExternalAuthenticationCredentialsProvider = new Lazy<IExternalAuthenticationCredentialsProvider<GcpCredentials>>(() => new GcpAuthenticationCredentialsProvider(_httpClientHelper), isThreadSafe: true);
        }

        public IExternalAuthenticationCredentialsProvider<AwsCredentials> Aws => _awsExternalAuthenticationCredentialsProvider.Value;
        public IExternalAuthenticationCredentialsProvider<GcpCredentials> Gcp => _gcpExternalAuthenticationCredentialsProvider.Value;
    }
}
