<template>
    <div class="slider-container">
      <div class="slide">
        <img src="../assets/images/man1.jpg" alt="slide 1" style="transform: scaleX(-1);">
        <div class="slide-content">
          <h2 style="font-size: 38px;">ШИРОКИЙ<br>АССОРТИМЕНТ<br>ОДЕЖДЫ</h2>
          <p>Одежда от известных брендов у нас в каталоге.<br>Только качественные вещи.</p>
          <a href="#" class="catalog_btn">ПЕРЕЙТИ В КАТАЛОГ</a>
        </div>
      </div>
    </div>
</template>
<script>

</script>
<style>
    /* Slider */
.slider-container {
  width: 85%;
  margin: 30px auto;
  height: 60vh;
  position: relative;
  /* нужно для абсолютного позиционирования текста */
  overflow: hidden;
  border-radius: 10px;
}

.slide img {
  width: 100%;
  height: auto;
  border-radius: 10px;
}

.slide-content {
  position: absolute;
  top: 28%;
  left: 5%;

  display: flex;
  flex-direction: column;
  gap: 15px;
  align-items: flex-start;
}

.catalog_btn {
  display: inline-block;
  background-color: black;
  padding: 20px;
  border-radius: 5px;
  font-size: 14px;
  color: white;
  text-decoration: none;
  text-align: center;
}
</style>