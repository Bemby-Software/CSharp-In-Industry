const colors = require('tailwindcss/colors');


module.exports = {
    prefix: '',
    purge: {
      content: [
        './src/**/*.{html,ts}',
      ]
    },
    darkMode: 'class', // or 'media' or 'class'
    theme: {
      colors: {
        cyan: colors.cyan,
        gray: colors.gray,
        green: colors.green,
        purple: colors.purple,
        yellow: colors.yellow,
        'light-blue': colors.lightBlue,
        blue: colors.blue,
        black: colors.black,
        white: colors.white,
        red: colors.red
      },
      extend: {
        screen: {
          'xs': '475px  '
        }
      },
    },
    variants: {
      extend: {
        borderStyle: ['hover', 'focus'],
      },
    },
    plugins: [require('@tailwindcss/forms'),require('@tailwindcss/typography')],
};
