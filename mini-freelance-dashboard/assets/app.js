// Shared helpers
function showLoader(show) {
  $('#loader').toggleClass('hidden', !show);
}

async function fetchJSON(url) {
  const res = await fetch(url);
  if (!res.ok) throw new Error('Network error');
  return res.json();
}

// Theme
function applySavedTheme() {
  const theme = localStorage.getItem('theme') || 'light';
  if (theme === 'dark') document.body.classList.add('dark');
}
function toggleTheme() {
  document.body.classList.toggle('dark');
  localStorage.setItem('theme', document.body.classList.contains('dark') ? 'dark' : 'light');
}
$(function(){
  applySavedTheme();
  $('#themeToggle').on('click', toggleTheme);

  // Toastr defaults
  toastr.options = {
    positionClass: 'toast-bottom-right',
    timeOut: 2000,
  };
});
