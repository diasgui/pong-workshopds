defmodule PongBackend.Guardian do
  use Guardian, otp_app: :pong_backend

  alias PongBackend.Repo
  alias PongBackend.Accounts.Player

  def subject_for_token(user = %Player{}, _claims), do: {:ok, "Player:#{user.id}"}
  def subject_for_token(_, _), do: {:error, "Unknown resource type"}

  def resource_from_claims(%{"sub" => "Player:" <> uid_str}), do: {:ok, Repo.get(Player, uid_str)}
  def resource_from_claims(_), do: {:error, "Unknown resource type"}
end