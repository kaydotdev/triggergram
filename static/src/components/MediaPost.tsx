import {
    Box,
    Typography
} from '@material-ui/core';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import { Skeleton } from '@material-ui/lab';

import { decimalMetric } from '../services/numerical';
import TimeAgo from 'timeago-react';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        container: {
            width: 'calc(100vw / 3 - 56px)',
            minWidth: '256px',
            maxWidth: '1024px',
            margin: theme.spacing(3)
        },
        photoWrapper: {
            position: 'relative',
            paddingBottom: '56.2%',
            marginBottom: '8px'
        },
        photo: {
            position: 'absolute',
            objectFit: 'cover',
            width: '100%',
            height: '100%'
        }
    }),
);

interface MediaPostProps {
    src?: string,
    title?: string,
    account?: string,
    views?: number,
    createdAt?: Date
}

function MediaPost(props: MediaPostProps) {
    const classes = useStyles();

    return (
        <div>
            {
            props.src ? (
                <div className={classes.container}>
                    <div className={classes.photoWrapper}>
                        <img className={classes.photo} alt={props.title} src={props.src} />
                    </div>
                    <Box>
                    <Typography gutterBottom variant="body2">
                        {props.title}
                    </Typography>
                    <Typography display="block" variant="caption" color="textSecondary">
                        {props.account}
                    </Typography>
                    <Typography variant="caption" color="textSecondary">
                        {decimalMetric(props.views || 0) + " views • "}
                        <TimeAgo datetime={props.createdAt?.toUTCString() || new Date().toUTCString()} locale='en_us' />
                    </Typography>
                    </Box>
                </div>
            ) : (
                <div className={classes.container}>
                    <Skeleton variant="rect" className={classes.photoWrapper} />
                    <Skeleton />
                    <Skeleton width="60%" />
                </div>
            )
            }
        </div>
    );
}

export default MediaPost;
