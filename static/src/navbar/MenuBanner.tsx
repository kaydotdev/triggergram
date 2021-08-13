import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import AppBanner from './banner.svg';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        banner: {
            flexGrow: 1,
            maxWidth: '150px',
            marginTop: '5px'
        }
    }),
);

function MenuBanner() {
    const classes = useStyles();

    return (<img src={AppBanner} className={classes.banner} alt="TRIGGERGRAM" />);
}

export default MenuBanner;
