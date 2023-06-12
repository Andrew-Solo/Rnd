import * as React from "react";
import {Color, Tone} from "./constants/colors";
import {createTheme} from "@mui/material";
import { Link as RouterLink } from 'react-router-dom';

const LinkBehavior = React.forwardRef((props, ref) => {
  const { href, ...other } = props;
  return <RouterLink data-testid="custom-link" ref={ref} to={href} {...other} />;
});

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
        light: Color.White[Tone.Lighter],
        main: Color.White[Tone.Normal],
        dark: Color.White[Tone.Darker],
      },
      warning: {
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
        light: Color.White[Tone.White],
        main: Color.White[Tone.Light],
        dark: Color.White[Tone.Normal],
      },
      text: {
        primary: Color.White[Tone.White],
        secondary: Color.White[Tone.Light],
        disabled: Color.White[Tone.Normal],
      },
      background: {
        default: Color.Aqua[Tone.Background]
      },
    },
    typography: {
      fontSize: 16,
      fontWeightLight: 100,
      fontWeightRegular: 300,
      fontWeightMedium: 400,
      fontWeightBold: 500,
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
      body2: {
        fontSize: 16,
        fontWeight: 500,
      },
      caption: {
        fontSize: 12,
        fontWeight: 300
      },
      button: {
        fontSize: 16,
        fontWeight: 400,
        textTransform: "none",
      }
    },
    spacing: 8,
    shape: {
      borderRadius: 8
    },
    components: {
      MuiTypography: {
        defaultProps: {
          variantMapping: {
            caption: 'p'
          },
        },
      },
      MuiLink: {
        defaultProps: {
          component: LinkBehavior,
        },
      },
      MuiButtonBase: {
        defaultProps: {
          LinkComponent: LinkBehavior,
        },
      },
    },
  }
}

