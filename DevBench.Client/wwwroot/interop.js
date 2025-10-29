window.browserJsFunctions = {
    getLanguage: () => {
        return navigator.language || navigator.userLanguage;
    }
};

window.devbench = window.devbench || {};
window.devbench.scrollToId = (id) => {
    const el = document.getElementById(id);
    if (!el) return false;
    el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    return true;
};