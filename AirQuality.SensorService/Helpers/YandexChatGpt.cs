﻿using AirQuality.Core.Loggers;
using Zefirrat.YandexGpt.Abstractions;

namespace AirQuality.SensorService.Helpers;

public class YandexChatGpt
{
    private readonly IYaPrompter _prompter;
    private readonly ILogger<YandexChatGpt> _log;

    public YandexChatGpt(IYaPrompter prompter, ILogger<YandexChatGpt> log)
    {
        _prompter = prompter;
        _log = log;
    }

    public async Task<string> GetAdvices(string locationName, float avgTemperature, int avgHumidity,
        int avgPm_1, int avgPm_2_5, int avgPm_10, int avgCo, int avgPressure)
    {
        try
        {
            var requestText = $"В городе {locationName} " +
                $"сейчас средняя температура воздуха {avgTemperature} градусов," +
                $"влажность воздуха {avgHumidity}%," +
                $"содержание pm 1 в воздухе {avgPm_1}," +
                $"содержание pm 2.5 в воздухе {avgPm_2_5}," +
                $"содержание pm 10 в воздухе {avgPm_10}," +
                $"содержание co в воздухе {avgCo}," +
                $"и среднее давление {avgPressure}," + 
                $"что ты можешь сказать об этом и какие дашь советы как лучше одеться?";

            var response = await _prompter.SendAsync(requestText);

            return response;
        }
        catch (Exception ex)
        {
            _log.LogError(LoggerMessages.Error(ex.Message.ToString()));

            return "Сегодня без советов";
        }
    }
}
