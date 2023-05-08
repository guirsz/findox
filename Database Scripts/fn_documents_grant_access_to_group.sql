CREATE OR REPLACE FUNCTION public.fn_documents_grant_access_to_group(
	in_document_id  	uuid,
	in_group_id  		integer,
	in_granted_date		timestamp without time zone,
	in_granted_by 		integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	INSERT INTO public.grant_access_groups (document_id, group_id, granted_date, granted_by)
	VALUES (in_document_id, in_group_id, in_granted_date, in_granted_by);	

$BODY$;

ALTER FUNCTION public.fn_documents_grant_access_to_group(uuid, integer, timestamp without time zone, integer)
    OWNER TO postgres;
