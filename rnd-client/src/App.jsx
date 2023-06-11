import {Box, CssBaseline, ThemeProvider} from "@mui/material";
import {Theme} from "theme";
import Router from "views/routes/Router";
import StoreProvider from "stores/StoreProvider";

export default function App () {
  return (
    <StoreProvider>
      <ThemeProvider theme={Theme}>
        <CssBaseline/>
        <Box className="app">
          <Router/>
        </Box>
      </ThemeProvider>
    </StoreProvider>
  );
}