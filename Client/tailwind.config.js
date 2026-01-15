/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./**/*.{html,cshtml,razor}"
  ],
  watch: [
    "./**/*.{html,cshtml,razor}"
  ],
  important: true, // important to not let bootstrap styles override tailwind
  theme: {
    extend: {
      overflow: {
        'clip': 'clip',
        'scroll': 'scroll',
        'hidden': 'hidden',
      },
      "overflow-x": {
        auto: "auto",
        scroll: "scroll",
        hidden: "hidden",
      },
      "overflow-y": {
        auto: "auto",
        scroll: "scroll",
        hidden: "hidden",
      }
    },
  },
  plugins: [
    function ({ addUtilities }) {
      addUtilities({
        '.scrollbar-hide': {
          /* For Chrome, Safari, and Opera */
          '&::-webkit-scrollbar': {
            display: 'none',
          },
          /* For IE, Edge and Firefox */
          '-ms-overflow-style': 'none',  /* IE and Edge */
          'scrollbar-width': 'none',     /* Firefox */
        },
      });
    }
  ],
}
