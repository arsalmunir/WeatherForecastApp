<template>
    <div class="weather-card">
        <div class="weather-card--overall">
        <h6>{{date}}</h6>
        </div>
        <div class="weather-card--temprature">
            <p class="font-weight-bold weather-card--temprature__width"> {{weather.avgTemperature}}°C </p>
            <WeatherIcon :icon="'temprature'"> </WeatherIcon> 
            
        </div>
        <div class="weather-card--humidity">
            <p class="font-weight-bold weather-card--humidity__width"> {{weather.avgHumidity}} % </p>
            <WeatherIcon :icon="'drop'"> </WeatherIcon> 
        </div>
        <div class="weather-card--windspeed">
            <p class="font-weight-bold weather-card--windspeed__width"> {{weather.avgWindSpeed}} m/s </p>
            <WeatherIcon :icon="'wind'"> </WeatherIcon> 
        </div>
    </div>
</template>

<script>
 import WeatherIcon from "./WeatherIcon.vue"
 export default {
    name: "WeatherCard",
    components:{
        WeatherIcon
    },
    props:{
        weather: {},
        date: null
    }
}
</script>

<style lang="scss" scoped>
.weather-card {
    width: 10vw;
    height: 50vh;
    min-height: 370px;
    min-width: 92px;
    border:2px solid black;
    background: lightcyan;
    display: flex;
    flex-direction: column;
    border-radius: 10px;
    margin: 0 8px;
    padding: 10px 10px;
    justify-content: space-between;
    text-align: right;

    &--overall, &--humidity, &--temprature, &--windspeed{
        display: flex;
        flex-direction: row;
        justify-content: center;
        &__width {
            width: 90%
        }
    }
}
</style>