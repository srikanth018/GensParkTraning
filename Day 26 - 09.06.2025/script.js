document.addEventListener("DOMContentLoaded", function () {
  const menuIcon = document.querySelector(".menu-toggle");
  const mobileMenu = document.querySelector(".nav-links-menu");

  menuIcon.addEventListener("click", () => {
    mobileMenu.classList.toggle("show");
  });
});
