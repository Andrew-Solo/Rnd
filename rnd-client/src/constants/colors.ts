type ColorMap = {[name: number]: string}

export const Color: {[name: string]: ColorMap} = {
  Aqua: {
    100: '#40FFDC',
    200: '#40FFDC',
    300: '#40FFDC',
    400: '#30F2CF',
    500: '#17E5BF',
    600: '#0ACCA8',
    700: '#00BF9C',
    800: '#00997D',
    900: '#00735E',
    950: '#181F1E',
  },
  Sand: {
    100: '#FFE48C',
    200: '#FFDB66',
    300: '#FFD240',
    400: '#F2C530',
    500: '#E5B717',
    600: '#CC9F0A',
    700: '#BF9300',
    800: '#997600',
    900: '#735800',
  },
  Mist: {
    100: '#FB8CFF',
    200: '#FA66FF',
    300: '#F940FF',
    400: '#EC30F2',
    500: '#DE17E5',
    600: '#C60ACC',
    700: '#B900BF',
    800: '#940099',
    900: '#6F0073',
  },
  White: {
    100: '#FFFFFFFF',
    200: '#FFFFFFF2',
    300: '#FFFFFFE6',
    400: '#FFFFFFCC',
    500: '#FFFFFF99',
    600: '#FFFFFF66',
    700: '#FFFFFF33',
    800: '#FFFFFF1A',
    900: '#FFFFFF0D',
  },
}

type ToneMap = {[name: string]: number}

export const Tone: ToneMap = {
  White: 100,
  Lightest: 200,
  Lighter: 300,
  Light: 400,
  Normal: 500,
  Dark: 600,
  Darker: 700,
  Darkest: 800,
  Black: 900,
  Background: 950,
}