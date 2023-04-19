<template>
  <div class="container-">

      <div id="andrew" class="member">
        <div class="img">
          <img src="@/assets/andrew-hill.jpg">
        </div>
        <div class="info">
          <p class="name">Andrew Hill</p>
          <p><a href="https://www.linkedin.com/in/andrew-michael-hill/">LinkedIn</a></p>
        </div>
      </div>

      <div id="alex" class="member">
        <div class="img">
          <img src="@/assets/alex-bartosik.jpg">
        </div>
        <div class="info">
          <p class="name">Alexander Bartosik</p>
          <p><a href="https://www.linkedin.com/in/alexbartosik/">LinkedIn</a></p>
        </div>
      </div>


      <div id="angelia" class="member">
        <div class="img">
          <img src="@/assets/angelia-thompson.jpg">
        </div>
        <div class="info">
          <p class="name">Angelia Thompson</p>
          <p><a href="https://www.linkedin.com/in/andrealeah/">LinkedIn</a></p>
        </div>
      </div>

  </div>
</template>

<script>
export default {

}
</script>

<style>

  img {
      border-radius: 50%;
      height: 150px;
  }

  .container- {
    display: flex;
    flex-direction: column;
    gap: 50px;
    padding: 3rem;
    align-content: center;
    margin-top: 2rem;
    background-color: white;
    border: thin solid rgb(201, 201, 201);
    box-shadow: 5px 5px 5px rgba(0, 0, 0, .15);
    border-radius: 10px;
    max-width: 35rem;
  }

  .member {
    display: flex;
    justify-content: center;
    gap: 50px;
  }

  .info {
    width: 300px;
    height: auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  .name {
    font-weight: bold;
    font-size: 26px;
    text-align: center;
  }

</style>