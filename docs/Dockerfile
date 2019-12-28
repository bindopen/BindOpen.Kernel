############
### prod ###
############

# base image
FROM nginx:1.16.0-alpine

# copy artifact build from the 'build environment'
COPY ./_site /usr/share/nginx/html

# expose port 80
EXPOSE 80

# run nginx
ENTRYPOINT ["nginx", "-g", "daemon off;"]