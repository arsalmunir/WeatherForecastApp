<template>
  <div class='weather'>
    <div class="weather--city input-group mx-auto w-25">
      <input type="search" class="form-control" v-model="location" placeholder="city/zipCode"/>
      <button @click="search" class="btn btn-primary" :disabled='isDisabled'> search </button>
    </div>
   
    <div v-if="!errorMessage && !isLoading">    
      <div class="weather--daily">
        <div >
          <h1> {{this.searchLocation}}</h1>
          <h3>{{date}}</h3>
          <ul class="list-group" style="text-align:end">
            <li class="list-group-item d-flex justify-content-center align-items-center"  > 
              <p class="w-50">{{weatherData.temp}} °C</p>
                <WeatherIcon icon="temprature"></WeatherIcon>
            </li>
            <li class="list-group-item d-flex justify-content-center align-items-center">
              <p class="w-75">{{weatherData.temp_Max}}°C / {{weatherData.temp_Min}}°C</p>
                <WeatherIcon icon="tempratureHalf" ></WeatherIcon>
            </li>
            <li class="list-group-item d-flex justify-content-center align-items-center">
            <p class="w-50"> {{weatherData.humidity}} %</p>
              <WeatherIcon icon="DropletHalf" ></WeatherIcon>
            </li>
          </ul>
        </div>
    </div>
    
    <div class="weather--week">
      <li v-for="(data,index) in weeklyData" :key="data">
        <WeatherCard :weather="data" :date="index" ></WeatherCard>
      </li>
    </div>
    </div>
   
    <div v-if="errorMessage && !isLoading" class="alert alert-danger mx-auto w-50 mt-5" role="alert">
          {{this.errorMessage}}
    </div>
    
    <div v-if="isLoading" class="spinner-border mt-5 " role="status">
      <span class="sr-only"></span> 
    </div>
  </div>
</template>

<script>
  import WeatherCard from './WeatherCard.vue';
  import WeatherIcon from './WeatherIcon.vue'
  import axios from 'axios'
  export default {
    name: 'WeatherComponent',
    components: {
      WeatherCard,
      WeatherIcon
    },
    computed:{
      isDisabled(){
          return this.location == ''? true : false
      }
    },
    data() {
      return {
        date: null,
        location:'Passau',
        weatherData: {},
        weeklyData: [],
        searchLocation:'Passau',
        errorMessage:'',
        hasError: false,
        isLoading: false
      }
    },
    mounted(){
      this.date =  new Date().toISOString().split("T")[0]
      this.search();
    },
    methods: {
        async search() {
          this.isLoading = true;
          this.hasError = false;
          this.errorMessage = ''
          
          try {
            const apiType  = this.location.match(/[A-Za-z]+/g) ? `city=${this.location}`: `city""=&zip=${this.location}`
            const weatherData = await axios.get(`${process.env.VUE_APP_API_ENDPOINT}WeatherForecast/weather?${apiType}`);
            const {wind, main, name} = weatherData.data;
            const weeklyData =  await axios.get(`${process.env.VUE_APP_API_ENDPOINT}WeatherForecast/forecast?${apiType}`);
            this.searchLocation = name;
            this.location = '';
          
            this.weatherData = main;
            this.windspeed = wind.speed;
            this.weeklyData = weeklyData.data;
          } 
          catch(e){
            this.hasError = true
            if(e.response.status)
              this.errorMessage= e.response.data.message;
          }
          finally{
            this.isLoading = false
          }
        }
    }
  }
</script>

<style lang="scss" scoped>
.weather {
  width: 100%;
  &--daily {
    display: flex;
    flex-direction: row;
    justify-content: center;
    margin: 10px;
    .list-group-item {
      border: none;
    }
  } 
  
  &--week {
    display: flex;
    flex-direction: row ;
    justify-content: space-evenly;
   
    li {
      list-style: none
    };
  }
}

</style>
