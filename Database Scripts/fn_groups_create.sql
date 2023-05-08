CREATE OR REPLACE FUNCTION public.fn_groups_create(
	in_group_name character varying,
	in_deleted boolean,
	in_created_date timestamp without time zone,
	in_created_by integer)
    RETURNS integer
    LANGUAGE SQL
AS $BODY$

	INSERT INTO public.groups(group_name, deleted, created_date, created_by, updated_date, updated_by)
	VALUES (in_group_name, in_deleted, in_created_date, in_created_by, in_created_date, in_created_by)
	RETURNING group_id;

$BODY$;

ALTER FUNCTION public.fn_groups_create(character varying, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
