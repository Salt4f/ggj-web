window.addEventListener('DOMContentLoaded', () => {
    let i = 0;
    const s = document.getElementById('mainNav'), e = s.clientHeight;
    window.addEventListener('scroll', function () {
        const t = -1 * document.body.getBoundingClientRect().top;
        t < i ? t > 0 && s.classList.contains('is-fixed') ? s.classList.add('is-visible') : s.classList.remove('is-visible', 'is-fixed') : (s.classList.remove(['is-visible']), t > e && !s.classList.contains('is-fixed') && s.classList.add('is-fixed')), i = t;
    });

    tsParticles.loadJSON('particles-js', window.location.origin + '/particles/particles_admin.json').then(function (p) {
        console.log('callback - particles.js config loaded');
    });
});