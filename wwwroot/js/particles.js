window.addEventListener('DOMContentLoaded', () => {
    tsParticles.loadJSON('particles-js', 'particles.json').then(function (p) {
        console.log('callback - particles.js config loaded');
    });
});