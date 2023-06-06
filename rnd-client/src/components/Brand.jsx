import {Box, Button} from "@mui/material";
import {ReactComponent as BrandSvg} from "../svg/brand.svg";

export default function Brand() {
  return (
    <Button href="/app" variant="text" color="primary" sx={{height: 80, padding: 0}}>
      <Box display="flex" justifyContent="center" alignItems="center">
        <BrandSvg/>
      </Box>
    </Button>
  );
}