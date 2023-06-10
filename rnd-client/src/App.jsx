import {Box, CssBaseline, ThemeProvider} from "@mui/material";
import {Theme} from "./theme";
import Router from "./views/Router";

export default function App () {
  return (
    <ThemeProvider theme={Theme}>
      <CssBaseline/>
      <Box className="app">
        <Router/>
      </Box>
    </ThemeProvider>
  );
}