import {
  CssBaseline,
  Grid,
  Typography
} from '@material-ui/core';
import {
  createTheme,
  ThemeProvider,
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import { indigo, blue } from '@material-ui/core/colors';
import NavBar from './components/NavBar';
import SearchBar from './components/SearchBar';
import MediaGrid from './components/MediaGrid';
import Footer from './components/Footer';


const theme = createTheme({
  palette: {
    type: 'light',
    primary: indigo,
    secondary: blue
  }
});


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        updates: {
            marginLeft: '24px'
        },
        mediaGrid: {
            height: 'calc(100vh - 294px)',
            paddingTop: '5px',
            overflowY: 'auto'
        }
    }),
);


function App() {
  const classes = useStyles();

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <NavBar />
      <Grid
        container
        direction="column"
        justifyContent="flex-start"
        alignItems="stretch"
      >
        <SearchBar />
        <Typography
          gutterBottom
          variant="h6"
          className={classes.updates}
        >
          Recent updates
        </Typography>
        <div className={classes.mediaGrid}><MediaGrid /></div>
        <Footer />
      </Grid>
    </ThemeProvider>
  );
}

export default App;
