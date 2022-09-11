<template>
  <div>
    <BIconCloudDrizzle class="icon" v-if="icon == 'rainy'" />
    <BIconDropletHalf class="icon" v-if="icon == 'DropletHalf'" />
    <BIconCloudy class="icon" v-if="icon == 'cloudy'"  />
    <BIconDropletHalf class="icon" v-if="icon == 'drop'" />
    <BIconWind class="icon" v-if="icon == 'wind'" />
    <BIconThermometerSun class="icon" v-if="icon == 'temprature'"  />
    <BIconThermometerHalf class="icon" v-if="icon == 'tempratureHalf'" />
  </div>
</template>

<script>
export default {
  name:"WeatherIcon",
  props:{
    icon: {default: ""}
  }
}
</script>

<style>
  .icon {
    margin: 0 10px;
    width: 40px;
    height:40px  
  }
  
</style>
