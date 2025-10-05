window.fittrack = {
    getTheme: () => localStorage.getItem('fittrack.theme') || 'light',
    setTheme: (name) => {
      localStorage.setItem('fittrack.theme', name);
      document.documentElement.setAttribute('data-theme', name);
    },
    initTheme: () => {
      const saved = localStorage.getItem('fittrack.theme');
      const t = saved || (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
      document.documentElement.setAttribute('data-theme', t);
    },
    downloadFile: (filename, content, mime) => {
      const blob = new Blob([content], { type: mime || 'text/plain' });
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url; a.download = filename; a.click();
      URL.revokeObjectURL(url);
    }
  };  