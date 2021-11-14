﻿using AdvertAPI.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly string _baseAddress;
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdvertApiClient(IConfiguration configuration, HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

            _baseAddress = configuration.GetSection("AdvertApi").GetValue<string>("BaseUrl");
        }

        public async Task<AdvertResponse> CreateAsync(CreateAdvertModel model)
        {
            var advertApiModel = _mapper.Map<AdvertModel>(model);

            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await _client.PostAsync(new Uri($"{_baseAddress}/create"),
                                                   new StringContent(jsonModel, Encoding.UTF8, "application/json"));
            var createAdvertResponse = await response.Content.ReadAsStringAsync();

            var createAdvertResponseObj = JsonConvert.DeserializeObject<CreateAdvertResponse>(createAdvertResponse);
            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponseObj);

            return advertResponse;
        }

        public async Task<bool> ConfirmAsync(ConfirmAdvertRequest model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await _client.PutAsync(new Uri($"{_baseAddress}/confirm"),
                                                  new StringContent(jsonModel, Encoding.UTF8, "application/json"));

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            var apiCallResponse = await _client.GetAsync(new Uri($"{_baseAddress}/all"));
            var allAdvertModels = await apiCallResponse.Content.ReadAsStringAsync();
            var advertModels = JsonConvert.DeserializeObject<List<AdvertModel>>(allAdvertModels);
            return advertModels.Select(x => _mapper.Map<Advertisement>(x)).ToList();
        }

        public async Task<Advertisement> GetAsync(string advertId)
        {
            var apiCallResponse = await _client.GetAsync(new Uri($"{_baseAddress}/{advertId}"));
            var fullAdvert = await apiCallResponse.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<AdvertModel>(fullAdvert);

            return _mapper.Map<Advertisement>(obj);
        }
    }
}