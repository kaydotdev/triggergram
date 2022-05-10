module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      spacing: {
        '128': '32rem',
      }
    }
  },
  daisyui: {
    themes: [
      {
        mytheme: {
          "primary": "#FE8F01",
          "secondary": "#4E91E6",
          "accent": "#37CDBE",
          "neutral": "#3D4451",
          "base-100": "#FFFFFF",
          "info": "#3ABFF8",
          "success": "#36D399",
          "warning": "#FBBD23",
          "error": "#F87272"
        },
      },
    ],
  },
  plugins: [require("daisyui")]
}
