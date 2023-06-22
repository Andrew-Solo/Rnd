import {Box, Button, CircularProgress} from "@mui/material";
import {ReactComponent as BrandSvg} from "../../assets/brand.svg";

export default function PageLoader() {
  return (
    <Box display="flex" width={1} height={1} justifyContent="center" alignItems="center">
      <CircularProgress size={80}/>
    </Box>
  );
}