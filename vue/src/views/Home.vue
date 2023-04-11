<template>
  <div class="home">

    <div id="main-content">
      <div class="main-content-text">
        <h1>Insulin Tracker</h1>
        <p>This application simulates that action of an insulin pump which delivers a continuous base level of insulin
          and then additional amounts based on food intake.</p>
        <p>
          Tracks blood sugar and insulin intake.
        </p>
      </div>
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="#f3f4f5" fill-opacity="1" d="M0,192L60,170.7C120,149,240,107,360,122.7C480,139,600,213,720,213.3C840,213,960,139,1080,101.3C1200,64,1320,64,1380,64L1440,64L1440,320L1380,320C1320,320,1200,320,1080,320C960,320,840,320,720,320C600,320,480,320,360,320C240,320,120,320,60,320L0,320Z" class="svg"></path>
      </svg>
    </div>
    <div id="cards">
      <div class="card-container">
      <div class="card">
        
        <img src="../assets/man.png" alt="" class="card-imgs">

        <div class="card-text">
          <p>Card Info</p>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
        </div>

      </div>

      <div class="card">
        
        <img src="../assets/man.png" alt="" class="card-imgs">

        <div class="card-text">
          <p>Card Info</p>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
        </div>

      </div>

      <div class="card">
        
        <img src="../assets/man.png" alt="" class="card-imgs">

        <div class="card-text">
          <p>Card Info</p>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
        </div>

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
  justify-content: flex-end;
}

.main-content-text {
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

svg {
  width: 100%;
  height: auto;
}

img.card-imgs {
  height: 70px;
  width: 70px;
}

#cards {
  display: flex;
  flex-direction: column;
}

.card-container {
  display: flex;
  justify-content: center;
  background-color: #F3F4F5;
  padding: 50px;
  gap: 50px;
}

.card {
  height: 300px;
  width: 200px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  padding: 15px;
  border-radius: 10px;
}

.card-text {
  display: flex;
  flex-direction: column;
  text-align: center;
}


</style>