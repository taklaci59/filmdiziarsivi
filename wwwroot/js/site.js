document.addEventListener("DOMContentLoaded", () => {
    // 1. Theme Engine Logic
    const themeToggleButton = document.getElementById('theme-toggle');
    const htmlElement = document.documentElement;

    function setTheme(themeName) {
        if (themeName === 'dark') {
            htmlElement.setAttribute('data-theme', 'dark');
            if(themeToggleButton) themeToggleButton.innerHTML = '<i class="bi bi-sun-fill"></i> Ligh Mode';
        } else {
            htmlElement.removeAttribute('data-theme');
            if(themeToggleButton) themeToggleButton.innerHTML = '<i class="bi bi-moon-fill"></i> Dark Mode';
        }
        localStorage.setItem('theme', themeName);
        
        // Broadcast event for Chart.js to listen to theme changes
        window.dispatchEvent(new Event('themeChanged'));
    }

    // Initialize Theme
    const storedTheme = localStorage.getItem('theme');
    if (storedTheme) {
        setTheme(storedTheme);
    } else {
        const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        setTheme(prefersDark ? 'dark' : 'light');
    }

    if (themeToggleButton) {
        themeToggleButton.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-theme');
            setTheme(currentTheme === 'dark' ? 'light' : 'dark');
        });
    }

    // 2. ModalManager Logic (A11y + Scroll-lock + iframe destruction)
    window.ModalManager = {
        overlay: null,
        content: null,
        lastFocusTrigger: null,

        init: function() {
            if(!document.getElementById('videoModalOverlay')) {
                const overlayHtml = `
                    <div id="videoModalOverlay" class="video-modal-overlay" role="dialog" aria-modal="true" tabindex="-1">
                        <div class="video-modal-wrapper" style="position:relative; width:100%; height:100%; display:flex; justify-content:center; align-items:center;">
                            <div class="video-modal-content">
                                <button id="videoModalClose" class="video-modal-close" aria-label="Kapat">&times;</button>
                                <div id="videoModalIframeContainer" style="width:100%; height:100%;"></div>
                            </div>
                        </div>
                    </div>
                `;
                document.body.insertAdjacentHTML('beforeend', overlayHtml);
            }
            this.overlay = document.getElementById('videoModalOverlay');
            this.content = document.getElementById('videoModalIframeContainer');
            
            document.getElementById('videoModalClose').addEventListener('click', () => this.close());
            this.overlay.addEventListener('click', (e) => {
                if (e.target === this.overlay || e.target.classList.contains('video-modal-wrapper')) {
                    this.close();
                }
            });
            
            // Allow Escape key closing
            document.addEventListener('keydown', (e) => {
                if (e.key === 'Escape' && this.overlay.classList.contains('active')) {
                    this.close();
                }
            });
        },

        open: function(trailerUrl, triggerElement) {
            if(!this.overlay) this.init();
            
            this.lastFocusTrigger = triggerElement;
            document.body.classList.add('scroll-lock');
            
            this.content.innerHTML = `<iframe class="video-iframe" src="${trailerUrl}?autoplay=1" allow="autoplay; encrypted-media" allowfullscreen></iframe>`;
            this.overlay.classList.add('active');
            
            // Manage focus for accessibility
            this.overlay.focus();
        },

        close: function() {
            if(!this.overlay) return;
            
            document.body.classList.remove('scroll-lock');
            this.overlay.classList.remove('active');
            
            // Destroy iframe entirely to kill video/audio
            setTimeout(() => {
                this.content.innerHTML = '';
            }, 300);
            
            // Return focus to the trigger that opened the modal
            if (this.lastFocusTrigger) {
                this.lastFocusTrigger.focus();
            }
        }
    };

    // Attach click events to all trailer buttons
    document.querySelectorAll('.btn-trailer').forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const url = this.getAttribute('data-url');
            window.ModalManager.open(url, this);
        });
    });
});
