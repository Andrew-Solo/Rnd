import {Color, Tone} from "./constants/Color";
import {createTheme} from "@mui/material";

export const Theme = createTheme(getThemeSettings());

function getThemeSettings () {
  return {
    palette: {
      mode: 'dark',
      primary: {
        light: Color.Aqua[Tone.Lighter],
        main: Color.Aqua[Tone.Normal],
        dark: Color.Aqua[Tone.Darker],
      },
      secondary: {
        light: Color.Sand[Tone.Lighter],
        main: Color.Sand[Tone.Normal],
        dark: Color.Sand[Tone.Darker],
      },
      error: {
        light: Color.Mist[Tone.Lighter],
        main: Color.Mist[Tone.Normal],
        dark: Color.Mist[Tone.Darker],
      },
      neutral: {
        light: Color.White[Tone.Lighter],
        main: Color.White[Tone.Normal],
        dark: Color.White[Tone.Darker],
      },
      text: {
        primary: Color.White[Tone.White],
        secondary: Color.White[Tone.Light],
        disabled: Color.White[Tone.Normal],
      },
      background: {
        default: Color.Aqua[Tone.Black]
      },
    },
    typography: {
      fontSize: 16,
      fontWeight: 400,
      brand: {
        fontFamily: "L&C Hairline HQ",
        fontSize: 32,
        color: "primary"
      },
      h1: {
        fontSize: 38,
        fontWeight: 100,
      },
      h2: {
        fontSize: 28,
        fontWeight: 300,
      },
      h3: {
        fontSize: 24,
        fontWeight: 300,
      },
      h4: {
        fontSize: 20,
        fontWeight: 300,
      },
      h5: {
        fontSize: 16,
        fontWeight: 300,
      },
      h6: {
        fontSize: 16,
        fontWeight: 400,
      },
      subtitle1: {
        fontSize: 16,
        fontWeight: 300,
      },
      body1: {
        fontSize: 16,
        fontWeight: 400,
      },
      caption: {
        fontSize: 16,
        fontWeight: 300,
      },
      button: {
        fontSize: 16,
        fontWeight: 400,
        textTransform: "none",
      }
    }
  }
}