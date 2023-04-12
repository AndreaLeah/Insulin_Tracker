<template>
  <div class="home">

    <div id="main-content">
      <div class="main-content-text">
        <!-- <h1>Insulin Tracker</h1> -->
        <h1>Insulin Control, </h1> 
        <h1> Your Way</h1>
        <button class="btn" @click="$router.push('register')" v-if="!$store.state.token">Get Started</button>
        <button class="btn" @click="$router.push('Profile')" v-if="$store.state.token">View My Information</button>

      </div>
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="#f3f4f5" fill-opacity="1" d="M0,192L60,170.7C120,149,240,107,360,122.7C480,139,600,213,720,213.3C840,213,960,139,1080,101.3C1200,64,1320,64,1380,64L1440,64L1440,320L1380,320C1320,320,1200,320,1080,320C960,320,840,320,720,320C600,320,480,320,360,320C240,320,120,320,60,320L0,320Z" class="svg"></path>
      </svg>
    </div>
    <div id="cards">
      <div class="card-container">
      <div class="card">
        
        <img src="../assets/insulin.png" alt="icon of insulin vial and syringe" class="card-imgs">

        <div class="card-text">
          <p class="title">Track Insulin</p>
          <p>Set and deliver a constant bolus of insulin set by your healthcare provider.</p>
        </div>

      </div>

      <div class="card">
        
        <img src="../assets/check.png" alt="icon of blood glucose machine checking blood sugar from a hand" class="card-imgs">

        <div class="card-text">
          <p class="title">Check Blood Glucose</p>
          <p>Check blood glucose levels and receive alerts when out of range.</p>
        </div>

      </div>

      <div class="card">
        
        <img src="../assets/adjust.png" alt="icon of adjusting controls" class="card-imgs">

        <div class="card-text">
          <p class="title">Adjust</p>
          <p>Adjust insulin levels based on blood glucose readings and food intake.</p>
        </div>

      </div>
      </div>
    </div>

    <div class="info-container">

      <div class="info-content-container">
        <img src="../assets/doctors.jpg" alt="picture of doctors talking and looking at screen" class="doctors">
        <div class="info-text">
          <h4>Work Together, Better</h4>
          <p>Better control, monitor, and work in conjunction with your healthcare provider for better blood glucose control.</p>
        </div>
      </div>

    </div>


  </div>
</template>

<script>
export default {
  name: "home"
};
</script>

<style>

.HomeStyling {
  max-width: 100%;
  max-height: 100%;
  margin: 0;
  padding: 0;
}

.home {
  display: flex;
  flex-direction: column;
  height: 100%;
  width: 100%;
}

#main-content {
  background-image: linear-gradient(rgb(255, 195, 195, 0.5), rgb(233, 64, 64, 0.9)), url('../assets/home-page-running.jpg');
  background-repeat: no-repeat;
  background-size: 100%;
  max-width: 100%;
  max-height: 100%;
  width: 100%;
  display: flex;
  flex-direction: column;
  height: 500px;
  position: relative;
}

.main-content-text {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: relative;
  z-index: 4;
  text-align: center;

  justify-self: flex-start;
  padding-top: 100px;
}

.btn {
  margin-top: 30px;
  background-image: none;
  background-color: rgb(255, 255, 255);
  border-radius: 20px;
  font-weight: bold;
  color: rgb(138, 1, 1);
}


svg {
  width: 100%;
  height: auto;
  position: absolute;
  bottom: 0;
  left: 0;
  z-index: 1;
}

img.card-imgs {
  height: 70px;
  width: 70px;
  border-radius: 0;
}

#cards {
  display: flex;
  flex-direction: column;
}

.card-container {
  display: flex;
  justify-content: center;
  background-color: #F3F4F5;
  padding-bottom: 100px;
  gap: 50px;
}

.card {
  height: 250px;
  width: 200px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  padding: 15px;
  border-radius: 10px;
  box-shadow: rgba(0, 0, 0, .1) 7px 5px 10px;
}

.card-text {
  display: flex;
  flex-direction: column;
  text-align: center;
}

.title {
  font-weight: bold;
  color: black;
}

/* Info Container */

.info-content-container {
  height: 300px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 50px;
  text-align: center;
}

.doctors {
  border-radius: 20px;
  height: 250px;
  width: auto;
}

.info-text {
  width: 200px;
  height: 250px;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
}

h4 {
  color: rgb(191, 0, 0);
  font-weight: bold;
}



</style>